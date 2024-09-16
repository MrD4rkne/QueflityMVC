using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing.Matching;
using Microsoft.Extensions.Logging;
using Moq;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.Results;
using QueflityMVC.Application.Services;
using QueflityMVC.Application.ViewModels.Element;
using QueflityMVC.Application.ViewModels.Image;
using QueflityMVC.Application.ViewModels.Item;
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
                                                                   kit.OrderNo == orderNo)), Times.Once);

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

    [Fact]
    public async Task Get_GetDetailsVmAsync_ReturnSuccess()
    {
        // Arrange
        int kitId = 1;
        string name = "Kit";
        string description = "Description";
        string altDescription = "AltDescription";
        string fileUrl = "fileUrl";
        bool shouldBeShown = true;
        uint orderNo = 2;

        int itemId = 1;
        string itemName = "Item";
        int itemCategoryId = 1;
        string itemCategoryName = "Category";
        string itemFileUrl = "itemFileUrl";
        string itemAltDescription = "itemAltDescription";
        decimal itemPrice = 3;
        bool itemShouldBeShown = true;

        Item item = new Item
        {
            Id = itemId,
            Name = itemName,
            CategoryId = itemCategoryId,
            Category = new Category
            {
                Id = itemCategoryId,
                Name = itemCategoryName
            },
            Image = new Image
            {
                FileUrl = itemFileUrl,
                AltDescription = itemAltDescription
            },
            ShouldBeShown = itemShouldBeShown
        };

        item.SetPrice(itemPrice);

        int elementId = 1;
        uint itemsAmount = 2;
        decimal pricePerItem = 3;

        ICollection<Element> elements = new List<Element>
        {
            new Element
            {
                Id = elementId,
                ItemsAmount = itemsAmount,
                PricePerItem = pricePerItem,
                KitId = kitId,
                ItemId = itemId,
                Item = item
            }
        };

        decimal kitPrice = elements.Sum(e => e.ItemsAmount * e.PricePerItem);

        Kit kit = new Kit()
        {
            Id = kitId,
            Name = name,
            Description = description,
            Image = new Image
            {
                FileUrl = fileUrl,
                AltDescription = altDescription
            },
            Elements = elements,
            ShouldBeShown = shouldBeShown,
            OrderNo = orderNo
        };

        _kitRepository.Setup(x => x.GetByIdAsync(kitId))
            .ReturnsAsync(kit);

        _kitRepository.Setup(x => x.ExistsAsync(kitId))
            .ReturnsAsync(true);
        _kitRepository.Setup(x => x.GetFullKitWithMembershipsByIdAsync(kitId))
            .ReturnsAsync(kit);
        
        _mapper.Setup(x => x.Map<KitDetailsVm>(It.IsAny<Kit>()))
            .Returns((Kit mappedKit)=>MapKitToDetailsVm(mappedKit));

        // Act
        var result = await _kitService.GetDetailsVmAsync(kitId);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldBeEquivalentTo(new KitDetailsVm()
        {
            Id = kitId,
            Name = name,
            Image = new ImageVm()
            {
                FileUrl = fileUrl,
                AltDescription = altDescription
            },
            Price = kitPrice,
            ShouldBeShown = shouldBeShown,
            ItemMemberships =
            [
                new()
                {
                    Id = elementId,
                    ItemsAmount = itemsAmount,
                    PricePerItem = pricePerItem,
                    Item = new ItemVm()
                    {
                        Id = itemId,
                        Name = itemName,
                        ShouldBeShown = itemShouldBeShown,
                        Price = itemPrice,
                        CategoryId = itemCategoryId,
                        Image = new ImageVm()
                        {
                            FileUrl = itemFileUrl,
                            AltDescription = itemAltDescription
                        }
                    },
                    KitId = kitId
                }
            ]
        });
    }

    [Fact]
    public async Task Get_GetDetailsVmAsync_OnNonExisting_ReturnError()
    {
        // Arrange
        int kitId = 1;
        
        _kitRepository.Setup(x => x.GetByIdAsync(kitId))
            .ReturnsAsync((Kit)null);
        _kitRepository.Setup(x => x.GetFullKitWithMembershipsByIdAsync(kitId))
            .ReturnsAsync((Kit)null);
        _kitRepository.Setup(x => x.ExistsAsync(kitId))
            .ReturnsAsync(false);
        
        // Act
        var result = await _kitService.GetDetailsVmAsync(kitId);
        
        // Assert
        result.IsFailure.ShouldBeTrue();
        result.Error.Code.ShouldBe(Errors.Kits.DoesNotExit.Code);
    }
    
    [Fact]
    public async Task Get_GetKitVmForEditAsync_ReturnSuccess()
    {
        // Arrange
        int kitId = 1;
        string name = "Kit";
        string description = "Description";
        string altDescription = "AltDescription";
        string fileUrl = "fileUrl";
        bool shouldBeShown = true;
        uint orderNo = 2;

        int itemId = 1;
        string itemName = "Item";
        int itemCategoryId = 1;
        string itemCategoryName = "Category";
        string itemFileUrl = "itemFileUrl";
        string itemAltDescription = "itemAltDescription";
        decimal itemPrice = 3;
        bool itemShouldBeShown = true;

        Item item = new Item
        {
            Id = itemId,
            Name = itemName,
            CategoryId = itemCategoryId,
            Category = new Category
            {
                Id = itemCategoryId,
                Name = itemCategoryName
            },
            Image = new Image
            {
                FileUrl = itemFileUrl,
                AltDescription = itemAltDescription
            },
            ShouldBeShown = itemShouldBeShown
        };

        item.SetPrice(itemPrice);

        int elementId = 1;
        uint itemsAmount = 2;
        decimal pricePerItem = 3;

        ICollection<Element> elements = new List<Element>
        {
            new Element
            {
                Id = elementId,
                ItemsAmount = itemsAmount,
                PricePerItem = pricePerItem,
                KitId = kitId,
                ItemId = itemId,
                Item = item
            }
        };

        decimal kitPrice = elements.Sum(e => e.ItemsAmount * e.PricePerItem);

        Kit kit = new Kit()
        {
            Id = kitId,
            Name = name,
            Description = description,
            Image = new Image
            {
                FileUrl = fileUrl,
                AltDescription = altDescription
            },
            Elements = elements,
            ShouldBeShown = shouldBeShown,
            OrderNo = orderNo
        };

        _kitRepository.Setup(x => x.GetByIdAsync(kitId))
            .ReturnsAsync(kit);

        _kitRepository.Setup(x => x.ExistsAsync(kitId))
            .ReturnsAsync(true);
        _kitRepository.Setup(x => x.GetFullKitWithMembershipsByIdAsync(kitId))
            .ReturnsAsync(kit);

        // Act
        var result = await _kitService.GetKitVmForEditAsync(kitId);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldBeEquivalentTo(new KitVm()
        {
            Id = kitId,
            Name = name,
            Description = description,
            Image = new ImageVm()
            {
                FileUrl = fileUrl,
                AltDescription = altDescription
            },
            Price = kitPrice,
            ShouldBeShown = shouldBeShown
        });
    }
    
    [Fact]
    public async Task Get_GetKitVmForEditAsync_OnNonExisting_ReturnError()
    {
        // Arrange
        int kitId = 1;
        
        _kitRepository.Setup(x => x.GetByIdAsync(kitId))
            .ReturnsAsync((Kit)null);
        _kitRepository.Setup(x => x.GetFullKitWithMembershipsByIdAsync(kitId))
            .ReturnsAsync((Kit)null);
        _kitRepository.Setup(x => x.ExistsAsync(kitId))
            .ReturnsAsync(false);
        
        // Act
        var result = await _kitService.GetKitVmForEditAsync(kitId);
        
        // Assert
        result.IsFailure.ShouldBeTrue();
        result.Error.Code.ShouldBe(Errors.Kits.DoesNotExit.Code);
    }

    [Fact]
    public async Task Update_EditElementAsync_OnDoesNotExist_ReturnError()
    {
        // Arrange
        int kitId = 1;
        int elementId = 1;
        int itemId = 1;
        uint itemsAmount = 2;
        decimal pricePerItem = 3;

        _kitRepository.Setup(x => x.ExistsAsync(kitId))
            .ReturnsAsync(false);
        
        ElementVm elementVm = new ElementVm()
        {
            Id = elementId,
            Item = new ItemVm()
            {
                Id = itemId
            },
            KitDetailsVm= new KitDetailsVm()
            {
                Id = kitId,
                ItemMemberships = [],
                Name = "Kit",
                Price = 0,
                ShouldBeShown = true,
                Image = new ImageVm()
                {
                    FileUrl = "fileUrl",
                    AltDescription = "AltDescription"
                }
            },
            ItemsAmount = itemsAmount,
            PricePerItem = pricePerItem
        };
        
        _kitRepository.Setup(x => x.GetElementAsync(elementId))
            .ReturnsAsync((Element)null);

        // Act
        var result = await _kitService.EditElementAsync(elementVm);

        // Assert
        result.IsFailure.ShouldBeTrue();
        result.Error.Code.ShouldBe(ErrorCodes.Elements.DOES_NOT_EXIST);
    }
    
    [Fact]
    public async Task Update_EditElementAsync_ReturnSuccess()
    {
        // Arrange
        int kitId = 1;
        int elementId = 1;
        int itemId = 1;
        uint itemsAmount = 2;
        decimal pricePerItem = 3;
        
        uint newItemsAmount = 2;
        decimal newPricePerItem = 3;
        
        Item item = new Item
        {
            Id = itemId,
            Name = "Item",
            CategoryId = 1,
            Category = new Category
            {
                Id = 1,
                Name = "Category"
            },
            Image = new Image
            {
                FileUrl = "itemFileUrl",
                AltDescription = "itemAltDescription"
            },
            ShouldBeShown = true
        };
        
        Kit kit = new Kit()
        {
            Id = kitId,
            Name = "Kit",
            Description = "Description",
            Image = new Image
            {
                FileUrl = "fileUrl",
                AltDescription = "AltDescription"
            },
            ShouldBeShown = true,
            Elements = []
        };

        Element elem = new Element()
        {
            Id = elementId,
            KitId = kitId,
            ItemId = itemId,
            ItemsAmount = itemsAmount,
            PricePerItem = pricePerItem,
            Item = item,
            Kit = kit
        };
        kit.Elements.Add(elem);

        _kitRepository.Setup(x => x.ExistsAsync(kitId))
            .ReturnsAsync(false);
        
        ElementVm elementVm = new ElementVm()
        {
            Id = elementId,
            Item = new ItemVm()
            {
                Id = itemId,
                Name = item.Name,
                Price = item.Price,
                ShouldBeShown = item.ShouldBeShown,
                CategoryId = item.CategoryId,
                Image = new ImageVm()
                {
                    FileUrl = item.Image.FileUrl,
                    AltDescription = item.Image.AltDescription
                }
            },
            KitDetailsVm= MapKitToDetailsVm(kit),
            ItemsAmount = newItemsAmount,
            PricePerItem = newPricePerItem
        };
        
        _kitRepository.Setup(x => x.GetElementAsync(elementId))
            .ReturnsAsync(new Element()
            {
                Id = elementId,
                KitId = kitId,
                ItemId = itemId,
                ItemsAmount = itemsAmount,
                PricePerItem = pricePerItem
            });
        _kitRepository.Setup(x => x.UpdateElementAsync(It.IsAny<Element>()));
        _kitRepository.Setup(x=>x.GetByIdAsync(kitId))
            .ReturnsAsync(kit);
        
        _itemRepository.Setup(x => x.GetByIdAsync(itemId))
            .ReturnsAsync(item);

        // Act
        var result = await _kitService.EditElementAsync(elementVm);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        _kitRepository.Verify(x=>x.UpdateElementAsync(It.Is<Element>(e=>e.Id==elementId &&
                                                                        e.KitId==kitId &&
                                                                        e.ItemId==itemId &&
                                                                        e.ItemsAmount==newItemsAmount &&
                                                                        e.PricePerItem==newPricePerItem)),Times.Once);
    }

    [Fact]
    public async Task Get_GetElementCount_OnNonExistingElement_ReturnError()
    {
        // Arrange
        int kitId = 1;
        int elementId = 1;
        
        _kitRepository.Setup(x => x.GetElementAsync(elementId))
            .ReturnsAsync((Element)null);
        
        // Act
        var result = await _kitService.GetElementCount(elementId);
        
        // Assert
        result.IsFailure.ShouldBeTrue();
        result.Error.Code.ShouldBe(ErrorCodes.Elements.DOES_NOT_EXIST);
    }
    
    [Fact]
    public async Task Get_GetElementCount_ReturnSuccess()
    {
        // Arrange
        int kitId = 1;
        int elementId = 2;
        
        Kit kit = new Kit()
        {
            Id = kitId,
            Name = "Kit",
            Description = "Description",
            Image = new Image
            {
                FileUrl = "fileUrl",
                AltDescription = "AltDescription"
            },
            ShouldBeShown = true,
            Elements = [
                new Element()
                {
                    Id = elementId,
                    KitId = kitId,
                    ItemId = 1,
                    ItemsAmount = 2,
                    PricePerItem = 3,
                    Item = new Item
                    {
                        Id = 1,
                        Name = "Item",
                        CategoryId = 1,
                        Category = new Category
                        {
                            Id = 1,
                            Name = "Category"
                        },
                        Image = new Image
                        {
                            FileUrl = "itemFileUrl",
                            AltDescription = "itemAltDescription"
                        },
                        ShouldBeShown = true
                    }
                },
                new Element()
                {
                    Id = elementId+1,
                    KitId = kitId,
                    ItemId = 2,
                    ItemsAmount = 2,
                    PricePerItem = 3,
                    Item = new Item
                    {
                        Id = 1,
                        Name = "Item",
                        CategoryId = 1,
                        Category = new Category
                        {
                            Id = 1,
                            Name = "Category"
                        },
                        Image = new Image
                        {
                            FileUrl = "itemFileUrl",
                            AltDescription = "itemAltDescription"
                        },
                        ShouldBeShown = true
                    }
                }
            ]
        };
        
        _kitRepository.Setup(x => x.GetByIdAsync(kitId))
            .ReturnsAsync(kit);
        _kitRepository.Setup(x => x.GetFullKitWithMembershipsByIdAsync(elementId))
            .ReturnsAsync(kit);
        
        // Act
        var result = await _kitService.GetElementCount(elementId);
        
        
        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldBe(kit.Elements.Count);
    }

    [Fact]
    public async Task Delete_DeleteElementAsync_OnNonExisting_ReturnError()
    {
        // Arrange
        int kitId = 1;
        int itemId = 2;
        
        _kitRepository.Setup(x => x.GetElementAsync(kitId,itemId))
            .ReturnsAsync((Element)null);
        
        // Act
        var result = await _kitService.DeleteElementAsync(kitId,itemId);
        
        // Assert
        result.IsFailure.ShouldBeTrue();
        result.Error.Code.ShouldBe(ErrorCodes.Elements.DOES_NOT_EXIST);
        _kitRepository.Verify(x=>x.DeleteElementAsync(It.IsAny<int>()),Times.Never);
    }
    
    [Fact]
    public async Task Delete_DeleteElementAsync_ReturnSuccess()
    {
        // Arrange
        int kitId = 1;
        int itemId = 2;
        int elementId = 3;

        Element element = new Element()
        {
            Id=elementId,
            ItemId = itemId,
            KitId = kitId,
            ItemsAmount = 4,
            PricePerItem = 5.99m
        };
        
        _kitRepository.Setup(x => x.GetElementAsync(kitId,itemId))
            .ReturnsAsync(element);
        
        // Act
        var result = await _kitService.DeleteElementAsync(kitId,itemId);
        
        // Assert
        result.IsSuccess.ShouldBeTrue();
        _kitRepository.Verify(x=>x.DeleteElementAsync(elementId),Times.Once);
    }
    
    
    [Fact]
    public async Task Delete_DeleteKitAsync_OnNonExisting_ReturnError()
    {
        // Arrange
        int kitId = 1;
        
        _kitRepository.Setup(x => x.GetByIdAsync(kitId))
            .ReturnsAsync((Kit)null);
        _kitRepository.Setup(x => x.ExistsAsync(kitId))
            .ReturnsAsync(false);
        
        // Act
        var result = await _kitService.DeleteKitAsync(kitId);
        
        // Assert
        result.IsFailure.ShouldBeTrue();
        result.Error.Code.ShouldBe(Errors.Kits.DoesNotExit.Code);
        _fileService.Verify(x=>x.DeleteImage(It.IsAny<string>()),Times.Never);
        _productRepository.Verify(x=>x.BulkUpdateOrderAsync(It.IsAny<uint>()),Times.Never);
    }

    [Fact]
    public async Task Delete_DeleteKitAsync_NotVisible_ReturnSuccess()
    {
        // Arrange
        int kitId = 1;
        Kit kit = new Kit()
        {
            Id = kitId,
            Name = "Kit",
            Description = "Description",
            Image = new Image
            {
                FileUrl = "fileUrl",
                AltDescription = "AltDescription"
            },
            Elements = []
        };
        
        _kitRepository.Setup(x => x.GetByIdAsync(kitId))
            .ReturnsAsync(kit);
        _kitRepository.Setup(x => x.ExistsAsync(kitId))
            .ReturnsAsync(true);
        
        // Act
        var result = await _kitService.DeleteKitAsync(kitId);
        
        // Assert
        result.IsSuccess.ShouldBeTrue();
        _kitRepository.Verify(x=>x.DeleteAsync(kitId),Times.Once);
        _fileService.Verify(x=>x.DeleteImage(kit.Image.FileUrl),Times.Once);
        _productRepository.Verify(x=>x.BulkUpdateOrderAsync(It.IsAny<uint>()),Times.Never);
    }
    
    [Fact]
    public async Task Delete_DeleteKitAsync_Visible_ReturnSuccess()
    {
        // Arrange
        int kitId = 1;
        uint orderNo = 2;
        Kit kit = new Kit()
        {
            Id = kitId,
            Name = "Kit",
            Description = "Description",
            Image = new Image
            {
                FileUrl = "fileUrl",
                AltDescription = "AltDescription"
            },
            ShouldBeShown = true,
            OrderNo = orderNo,
            Elements = []
        };
        
        _kitRepository.Setup(x => x.GetByIdAsync(kitId))
            .ReturnsAsync(kit);
        _kitRepository.Setup(x => x.ExistsAsync(kitId))
            .ReturnsAsync(true);
        
        // Act
        var result = await _kitService.DeleteKitAsync(kitId);
        
        // Assert
        result.IsSuccess.ShouldBeTrue();
        _kitRepository.Verify(x=>x.DeleteAsync(kitId),Times.Once);
        _fileService.Verify(x=>x.DeleteImage(kit.Image.FileUrl),Times.Once);
        _productRepository.Verify(x=>x.BulkUpdateOrderAsync(orderNo),Times.Once);
    }

    private KitDetailsVm MapKitToDetailsVm(Kit kit)
    {
        return new KitDetailsVm()
        {
            Id = kit.Id,
            Name = kit.Name,
            Price = kit.Price,
            Image = new ImageVm()
            {
                FileUrl = kit.Image.FileUrl,
                AltDescription = kit.Image.AltDescription
            },
            ShouldBeShown = kit.ShouldBeShown,
            ItemMemberships = kit.Elements.Select(e => new ElementForListVm()
            {
                Id = e.Id,
                ItemsAmount = e.ItemsAmount,
                PricePerItem = e.PricePerItem,
                Item = new ItemVm()
                {
                    Id = e.Item.Id,
                    Name = e.Item.Name,
                    Price = e.Item.Price,
                    ShouldBeShown = e.Item.ShouldBeShown,
                    CategoryId = e.Item.CategoryId,
                    Image = new ImageVm()
                    {
                        FileUrl = e.Item.Image.FileUrl,
                        AltDescription = e.Item.Image.AltDescription
                    }
                },
                KitId = e.KitId
            }).ToList()
        };
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
            ShouldBeShown = kit.ShouldBeShown,
            Price = kit.Price
        };
        return kitVm;
    }
}