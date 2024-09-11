using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing.Matching;
using Microsoft.Extensions.Logging;
using Moq;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.Results;
using QueflityMVC.Application.Services;
using QueflityMVC.Application.ViewModels.Image;
using QueflityMVC.Application.ViewModels.Kit;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;
using Serilog;
using Shouldly;

namespace QueflityMVC.Application.UnitTests.Services;

public class KitServiceTests
{
    private readonly Mock<IFileService> _fileService;
    private readonly Mock<IItemRepository> _itemRepository;
    private readonly Mock<IKitRepository> _kitRepository;
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<IProductRepository> _productRepository;
    private readonly Mock<ILogger<KitService>> _logger;
    private readonly IKitService _kitService;

    public KitServiceTests()
    {
        _fileService = new Mock<IFileService>();
        _itemRepository = new Mock<IItemRepository>();
        _kitRepository = new Mock<IKitRepository>();

        _mapper = new Mock<IMapper>();
        _mapper.Setup(mapper => mapper.Map<Kit>(It.IsAny<KitVm>()))
            .Returns((KitVm kitVm) => MapKitFromVm(kitVm));
        _mapper.Setup(mapper => mapper.Map<KitVm>(It.IsAny<Kit>()))
            .Returns((Kit kit) => MapKitToVm(kit));

        _productRepository = new Mock<IProductRepository>();
        _logger = new Mock<ILogger<KitService>>();
        _kitService = new KitService(_kitRepository.Object, _itemRepository.Object, _mapper.Object, _fileService.Object,
            _productRepository.Object, _logger.Object);
    }

    [Fact]
    public async Task Create_CreateKitAsync_NotVisible_ReturnSuccess()
    {
        // Arrange
        string name = "Kit";
        string description = "Description";
        string altDescription = "AltDescription";
        var kitVm = new KitVm
        {
            Name = name,
            Image = new ImageVm()
            {
                FormFile = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")),
                    0,
                    0,
                    "Data",
                    "dummy.txt"),
                AltDescription = altDescription
            },
            Description = description,
            ShouldBeShown = false,
            Id = 0
        };

        string fileUrl = "fileUrl";
        _fileService.Setup(x => x.UploadFileAsync(It.IsAny<IFormFile>()))
            .ReturnsAsync(fileUrl);

        _kitRepository.Setup(x => x.AddAsync(It.IsAny<Kit>()))
            .ReturnsAsync((Kit kit) => kit);

        // Act
        var result = await _kitService.CreateKitAsync(kitVm);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.Name.ShouldBe(name);
        result.Value.Description.ShouldBe(description);
        result.Value.Image.FileUrl.ShouldBe(fileUrl);
        result.Value.Image.AltDescription.ShouldBe(altDescription);

        _productRepository.Verify(x => x.GetNextOrderNumberAsync(), Times.Never);
        _fileService.Verify(x => x.UploadFileAsync(It.IsAny<IFormFile>()), Times.Once);
        _kitRepository.Verify(x => x.AddAsync(It.Is<Kit>(kit =>
            kit.ShouldBeShown == false &&
            kit.Description == description &&
            kit.Image.AltDescription == altDescription &&
            kit.Image.FileUrl == fileUrl
        )), Times.Once);
    }

    [Fact]
    public async Task Create_CreateKitAsync_Visible_ReturnSuccess()
    {
        // Arrange
        string name = "Kit";
        string description = "Description";
        string altDescription = "AltDescription";
        var kitVm = new KitVm
        {
            Name = name,
            Image = new ImageVm()
            {
                FormFile = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")),
                    0,
                    0,
                    "Data",
                    "dummy.txt"),
                AltDescription = altDescription
            },
            Description = description,
            ShouldBeShown = true,
            Id = 0
        };

        string fileUrl = "fileUrl";
        _fileService.Setup(x => x.UploadFileAsync(It.IsAny<IFormFile>()))
            .ReturnsAsync(fileUrl);

        _kitRepository.Setup(x => x.AddAsync(It.IsAny<Kit>()))
            .ReturnsAsync((Kit kit) => kit);

        uint orderNumber = 1;
        _productRepository.Setup(x => x.GetNextOrderNumberAsync())
            .ReturnsAsync(orderNumber);

        // Act
        var result = await _kitService.CreateKitAsync(kitVm);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.Name.ShouldBe(name);
        result.Value.Description.ShouldBe(description);
        result.Value.Image.FileUrl.ShouldBe(fileUrl);
        result.Value.Image.AltDescription.ShouldBe(altDescription);

        _productRepository.Verify(x => x.GetNextOrderNumberAsync(), Times.Once);
        _fileService.Verify(x => x.UploadFileAsync(It.IsAny<IFormFile>()), Times.Once);
        _kitRepository.Verify(x => x.AddAsync(It.Is<Kit>(kit =>
            kit.ShouldBeShown == true &&
            kit.Description == description &&
            kit.Image.AltDescription == altDescription &&
            kit.Image.FileUrl == fileUrl &&
            kit.OrderNo == orderNumber
        )), Times.Once);
    }

    [Fact]
    public async Task Create_CreateKitAsync_OnIFileServiceException_ReturnError()
    {
        // Arrange
        string name = "Kit";
        string description = "Description";
        string altDescription = "AltDescription";
        var kitVm = new KitVm
        {
            Name = name,
            Image = new ImageVm()
            {
                FormFile = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")),
                    0,
                    0,
                    "Data",
                    "dummy.txt"),
                AltDescription = altDescription
            },
            Description = description,
            ShouldBeShown = true,
            Id = 0
        };

        string fileUrl = "fileUrl";
        _fileService.Setup(x => x.UploadFileAsync(It.IsAny<IFormFile>()))
            .ThrowsAsync(new IOException());

        _kitRepository.Setup(x => x.AddAsync(It.IsAny<Kit>()))
            .ReturnsAsync((Kit kit) => kit);

        uint orderNumber = 1;
        _productRepository.Setup(x => x.GetNextOrderNumberAsync())
            .ReturnsAsync(orderNumber);

        // Act
        var result = await _kitService.CreateKitAsync(kitVm);

        // Assert
        result.IsFailure.ShouldBeTrue();
        result.Error.Code.ShouldBe(Errors.Files.FileUploadFailed.Code);

        _fileService.Verify(x => x.UploadFileAsync(It.IsAny<IFormFile>()), Times.Once);
        _kitRepository.Verify(x => x.AddAsync(It.IsAny<Kit>()), Times.Never);
    }

    [Fact]
    public async Task Edit_EditKitAsync_ImageNotChanged_NoVisibilityChange_ReturnSuccess()
    {
        // Arrange
        string name = "Kit";
        string description = "Description";
        string altDescription = "AltDescription";
        int id = 2;
        string fileUrl = "fileUrl";
        string maliciousFileUrl = "maliciousFileUrl";

        var kitVm = new KitVm
        {
            Name = name,
            Image = new ImageVm()
            {
                FileUrl = maliciousFileUrl,
                AltDescription = altDescription
            },
            Description = description,
            ShouldBeShown = false,
            Id = id
        };

        _kitRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(new Kit()
            {
                Id = id,
                Name = "OldName",
                Description = "OldDescription",
                Image = new Image
                {
                    FileUrl = fileUrl,
                    AltDescription = "OldAltDescription"
                },
                ShouldBeShown = false
            });
        _kitRepository.Setup(x => x.UpdateAsync(It.IsAny<Kit>()))
            .ReturnsAsync((Kit kit) => kit);
        _kitRepository.Setup(x => x.ExistsAsync(kitVm.Id))
            .ReturnsAsync(true);

        // Act
        var result = await _kitService.EditKitAsync(kitVm);

        // Assert
        result.IsSuccess.ShouldBeTrue();

        result.Value.ShouldBeEquivalentTo(new KitVm()
        {
            Id = id,
            Name = name,
            Description = description,
            Image = new ImageVm()
            {
                FileUrl = fileUrl,
                AltDescription = altDescription
            },
            ShouldBeShown = false
        });

        _fileService.Verify(x => x.UploadFileAsync(It.IsAny<IFormFile>()), Times.Never);
        _kitRepository.Verify(x => x.UpdateAsync(It.IsAny<Kit>()), Times.Once);
        _productRepository.Verify(x => x.GetNextOrderNumberAsync(), Times.Never);
    }

    [Fact]
    public async Task Edit_EditKitAsync_ImageChanged_NoVisibilityChange_ReturnSuccess()
    {
        // Arrange
        string name = "Kit";
        string description = "Description";
        string altDescription = "AltDescription";
        int id = 2;
        var kitVm = new KitVm
        {
            Name = name,
            Image = new ImageVm()
            {
                FormFile = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")),
                    0,
                    0,
                    "Data",
                    "dummy.txt"),
                AltDescription = altDescription
            },
            Description = description,
            ShouldBeShown = false,
            Id = id
        };

        string fileUrl = "fileUrl";
        string oldUrl = "oldUrl";
        _fileService.Setup(x => x.UploadFileAsync(It.IsAny<IFormFile>()))
            .ReturnsAsync(fileUrl);

        _kitRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(new Kit()
            {
                Id = id,
                Name = "OldName",
                Description = "OldDescription",
                Image = new Image
                {
                    FileUrl = oldUrl,
                    AltDescription = "OldAltDescription"
                },
                ShouldBeShown = false
            });
        _kitRepository.Setup(x => x.UpdateAsync(It.IsAny<Kit>()))
            .ReturnsAsync((Kit kit) => kit);
        _kitRepository.Setup(x => x.ExistsAsync(kitVm.Id))
            .ReturnsAsync(true);

        // Act
        var result = await _kitService.EditKitAsync(kitVm);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldBeEquivalentTo(new KitVm()
        {
            Id = id,
            Name = name,
            Description = description,
            Image = new ImageVm()
            {
                FileUrl = fileUrl,
                AltDescription = altDescription
            },
            ShouldBeShown = false
        });

        _fileService.Verify(x => x.DeleteImage(oldUrl), Times.Once);
        _fileService.Verify(x => x.UploadFileAsync(It.IsAny<IFormFile>()), Times.Once);

        _kitRepository.Verify(x => x.UpdateAsync(It.IsAny<Kit>()), Times.Once);
        _productRepository.Verify(x => x.GetNextOrderNumberAsync(), Times.Never);
    }

    [Fact]
    public async Task Edit_EditKitAsync_ImageChanged_VisibilityTrueToFalse_ReturnSuccess()
    {
        // Arrange
        string name = "Kit";
        string description = "Description";
        string altDescription = "AltDescription";
        int id = 2;
        var kitVm = new KitVm
        {
            Name = name,
            Image = new ImageVm()
            {
                FormFile = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")),
                    0,
                    0,
                    "Data",
                    "dummy.txt"),
                AltDescription = altDescription
            },
            Description = description,
            ShouldBeShown = false,
            Id = id
        };

        string fileUrl = "fileUrl";
        string oldUrl = "oldUrl";
        _fileService.Setup(x => x.UploadFileAsync(It.IsAny<IFormFile>()))
            .ReturnsAsync(fileUrl);

        uint orderNo = 2;
        _kitRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(new Kit()
            {
                Id = id,
                Name = "OldName",
                Description = "OldDescription",
                Image = new Image
                {
                    FileUrl = oldUrl,
                    AltDescription = "OldAltDescription"
                },
                ShouldBeShown = true,
                OrderNo = orderNo
            });
        _kitRepository.Setup(x => x.UpdateAsync(It.IsAny<Kit>()))
            .ReturnsAsync((Kit kit) => kit);
        _kitRepository.Setup(x => x.ExistsAsync(kitVm.Id))
            .ReturnsAsync(true);

        // Act
        var result = await _kitService.EditKitAsync(kitVm);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldBeEquivalentTo(new KitVm()
        {
            Id = id,
            Name = name,
            Description = description,
            Image = new ImageVm()
            {
                FileUrl = fileUrl,
                AltDescription = altDescription
            },
            ShouldBeShown = false
        });

        _fileService.Verify(x => x.DeleteImage(oldUrl), Times.Once);
        _fileService.Verify(x => x.UploadFileAsync(It.IsAny<IFormFile>()), Times.Once);

        _kitRepository.Verify(x => x.UpdateAsync(It.IsAny<Kit>()), Times.Once);
        _productRepository.Verify(x => x.GetNextOrderNumberAsync(), Times.Never);
        _productRepository.Verify(x => x.BulkUpdateOrderAsync(orderNo), Times.Once);
    }

    [Fact]
    public async Task Edit_EditKitAsync_ImageChanged_VisibilityFalseToTrue_ReturnSuccess()
    {
        // Arrange
        string name = "Kit";
        string description = "Description";
        string altDescription = "AltDescription";
        int id = 2;
        var kitVm = new KitVm
        {
            Name = name,
            Image = new ImageVm()
            {
                FormFile = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")),
                    0,
                    0,
                    "Data",
                    "dummy.txt"),
                AltDescription = altDescription
            },
            Description = description,
            ShouldBeShown = true,
            Id = id
        };

        string fileUrl = "fileUrl";
        string oldUrl = "oldUrl";
        _fileService.Setup(x => x.UploadFileAsync(It.IsAny<IFormFile>()))
            .ReturnsAsync(fileUrl);

        _kitRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(new Kit()
            {
                Id = id,
                Name = "OldName",
                Description = "OldDescription",
                Image = new Image
                {
                    FileUrl = oldUrl,
                    AltDescription = "OldAltDescription"
                },
                ShouldBeShown = false
            });
        _kitRepository.Setup(x => x.UpdateAsync(It.IsAny<Kit>()))
            .ReturnsAsync((Kit kit) => kit);
        _kitRepository.Setup(x => x.ExistsAsync(kitVm.Id))
            .ReturnsAsync(true);

        uint orderNo = 2;
        _productRepository.Setup(x => x.GetNextOrderNumberAsync())
            .ReturnsAsync(orderNo);

        // Act
        var result = await _kitService.EditKitAsync(kitVm);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldBeEquivalentTo(new KitVm()
        {
            Id = id,
            Name = name,
            Description = description,
            Image = new ImageVm()
            {
                FileUrl = fileUrl,
                AltDescription = altDescription
            },
            ShouldBeShown = true
        });

        _fileService.Verify(x => x.DeleteImage(oldUrl), Times.Once);
        _fileService.Verify(x => x.UploadFileAsync(It.IsAny<IFormFile>()), Times.Once);

        _kitRepository.Verify(x => x.UpdateAsync(It.Is<Kit>(kit => kit.Id == id &&
                                                                   kit.Name == name &&
                                                                   kit.Description == description &&
                                                                   kit.Image.FileUrl == fileUrl &&
                                                                   kit.Image.AltDescription == altDescription &&
                                                                   kit.ShouldBeShown == true &&
                                                                   kit.OrderNo==orderNo)), Times.Once);
        
        _productRepository.Verify(x => x.GetNextOrderNumberAsync(), Times.Once);
        _productRepository.Verify(x => x.BulkUpdateOrderAsync(orderNo), Times.Never);
    }

    [Fact]
    public async Task Edit_EditKitAsync_NonExisting_ReturnError()
    {
        // Arrange
        string name = "Kit";
        string description = "Description";
        string altDescription = "AltDescription";
        int id = 2;
        string fileUrl = "fileUrl";

        var kitVm = new KitVm
        {
            Name = name,
            Image = new ImageVm()
            {
                FileUrl = fileUrl,
                AltDescription = altDescription
            },
            Description = description,
            ShouldBeShown = false,
            Id = id
        };

        _kitRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((Kit)null);
        _kitRepository.Setup(x => x.ExistsAsync(kitVm.Id))
            .ReturnsAsync(false);

        // Act
        var result = await _kitService.EditKitAsync(kitVm);

        // Assert
        result.IsFailure.ShouldBeTrue();

        result.Error.Code.ShouldBe(Errors.Kits.DoesNotExit.Code);

        _fileService.Verify(x => x.UploadFileAsync(It.IsAny<IFormFile>()), Times.Never);
        _kitRepository.Verify(x => x.UpdateAsync(It.IsAny<Kit>()), Times.Never);
        _productRepository.Verify(x => x.GetNextOrderNumberAsync(), Times.Never);
    }
    
    [Fact]
    public async Task Edit_EditKitAsync_ImageChanged_VisibilityNotChanged_OnIFileServiceException_ReturnError()
    {
        // Arrange
        string name = "Kit";
        string description = "Description";
        string altDescription = "AltDescription";
        int id = 2;
        var kitVm = new KitVm
        {
            Name = name,
            Image = new ImageVm()
            {
                FormFile = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")),
                    0,
                    0,
                    "Data",
                    "dummy.txt"),
                AltDescription = altDescription
            },
            Description = description,
            ShouldBeShown = true,
            Id = id
        };
        
        string oldUrl = "oldUrl";
        _fileService.Setup(x => x.UploadFileAsync(It.IsAny<IFormFile>()))
            .ThrowsAsync(new Exception());

        _kitRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(new Kit()
            {
                Id = id,
                Name = "OldName",
                Description = "OldDescription",
                Image = new Image
                {
                    FileUrl = oldUrl,
                    AltDescription = "OldAltDescription"
                },
                ShouldBeShown = false
            });
        _kitRepository.Setup(x => x.UpdateAsync(It.IsAny<Kit>()))
            .ReturnsAsync((Kit kit) => kit);
        _kitRepository.Setup(x => x.ExistsAsync(kitVm.Id))
            .ReturnsAsync(true);

        uint orderNo = 2;
        _productRepository.Setup(x => x.GetNextOrderNumberAsync())
            .ReturnsAsync(orderNo);

        // Act
        var result = await _kitService.EditKitAsync(kitVm);

        // Assert
        result.IsFailure.ShouldBeTrue();
        result.Error.Code.ShouldBe(Errors.Files.FileUploadFailed.Code);
        
        _fileService.Verify(x => x.DeleteImage(oldUrl), Times.Never);
        _fileService.Verify(x => x.UploadFileAsync(It.IsAny<IFormFile>()), Times.Once);

        _kitRepository.Verify(x => x.UpdateAsync(It.IsAny<Kit>()), Times.Never);
        
        _productRepository.Verify(x => x.BulkUpdateOrderAsync(orderNo), Times.Never);
    }

    private Kit MapKitFromVm(KitVm kitVm)
    {
        Kit kit = new Kit
        {
            Id = kitVm.Id,
            Name = kitVm.Name,
            Description = kitVm.Description,
            Image = new Image
            {
                FileUrl = kitVm.Image.FileUrl,
                AltDescription = kitVm.Image.AltDescription
            },
            ShouldBeShown = kitVm.ShouldBeShown
        };
        return kit;
    }

    private KitVm MapKitToVm(Kit kit)
    {
        KitVm kitVm = new KitVm
        {
            Id = kit.Id,
            Name = kit.Name,
            Description = kit.Description,
            Image = new ImageVm
            {
                FileUrl = kit.Image.FileUrl,
                AltDescription = kit.Image.AltDescription
            },
            ShouldBeShown = kit.ShouldBeShown
        };
        return kitVm;
    }
}