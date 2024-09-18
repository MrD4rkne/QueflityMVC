using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.Results;
using QueflityMVC.Application.Services;
using QueflityMVC.Application.ViewModels.Component;
using QueflityMVC.Application.ViewModels.Image;
using QueflityMVC.Application.ViewModels.Item;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;
using Shouldly;

namespace QueflityMVC.Application.UnitTests.Services;

public class ItemServiceTests
{
    private readonly Mock<ICategoryRepository> categoryRepository;
    private readonly Mock<IComponentRepository> componentRepository;
    private readonly Mock<IFileService> fileService;
    private readonly Mock<IItemRepository> itemRepository;
    private readonly ItemService itemService;
    private readonly Mock<ILogger<ItemService>> logger;
    private readonly Mock<IMapper> mapper;
    private readonly Mock<IProductRepository> productRepository;

    public ItemServiceTests()
    {
        itemRepository = new Mock<IItemRepository>();

        mapper = new Mock<IMapper>();
        mapper.Setup(x => x.Map<Item>(It.IsAny<ItemVm>()))
            .Returns((ItemVm itemVm) => MapItemFromVm(itemVm));
        mapper.Setup(x => x.Map<ItemVm>(It.IsAny<Item>()))
            .Returns((Item item) => MapItemToVm(item));

        categoryRepository = new Mock<ICategoryRepository>();
        componentRepository = new Mock<IComponentRepository>();
        fileService = new Mock<IFileService>();
        productRepository = new Mock<IProductRepository>();
        logger = new Mock<ILogger<ItemService>>();

        itemService = new ItemService(
            itemRepository.Object,
            mapper.Object,
            categoryRepository.Object,
            componentRepository.Object,
            fileService.Object,
            productRepository.Object,
            logger.Object);
    }

    [Fact]
    public async Task Create_CreateItemAsync_ReturnsItemId_AssignsOrderNo()
    {
        // Arrange
        var name = "Test item";
        var price = 15.99m;
        var altDescription = "Test image";
        var categoryId = 1;

        var itemVm = new ItemVm
        {
            Name = name,
            Price = price,
            ShouldBeShown = true,
            Image = new ImageVm
            {
                FormFile = new FormFile(
                    new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")),
                    0,
                    0,
                    "Data",
                    "dummy.txt"),
                AltDescription = altDescription
            },
            CategoryId = categoryId
        };

        itemRepository.Setup(x => x.AddAsync(It.IsAny<Item>()))
            .ReturnsAsync((Item item)=>item);

        categoryRepository.Setup(x => x.ExistsAsync(1))
            .ReturnsAsync(true);

        string savedImageUrl = "https://www.example.com/image.jpg";
        fileService.Setup(x => x.UploadFileAsync(It.IsAny<IFormFile>()))
            .ReturnsAsync(savedImageUrl);

        int orderNo = 2;
        productRepository.Setup(x => x.GetNextOrderNumberAsync())
            .ReturnsAsync((uint)orderNo);

        // Act
        var result = await itemService.CreateItemAsync(itemVm);

        // Assert
        result.IsSuccess.ShouldBeTrue();

        itemRepository.Verify(x => x.AddAsync(It.Is<Item>((item) =>
                item.Name == name &&
                item.Price == price &&
                item.ShouldBeShown &&
                item.Image.FileUrl == savedImageUrl &&
                item.Image.AltDescription == altDescription &&
                item.CategoryId == categoryId &&
                item.OrderNo == orderNo
            ))
            , Times.Once);

        categoryRepository.Verify(x => x.ExistsAsync(categoryId), Times.AtLeastOnce);
        productRepository.Verify(x => x.GetNextOrderNumberAsync(), Times.Once);
        fileService.Verify(x => x.UploadFileAsync(It.IsAny<IFormFile>()), Times.Once);
    }

    [Fact]
    public async Task Create_CreateItemAsync_ReturnsItemId_DoesNotAssignOrderNo()
    {
        // Arrange
        var name = "Test item";
        var price = 15.99m;
        var altDescription = "Test image";
        var categoryId = 1;

        var itemVm = new ItemVm
        {
            Name = name,
            Price = price,
            ShouldBeShown = false,
            Image = new ImageVm
            {
                FormFile = new FormFile(
                    new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")),
                    0,
                    0,
                    "Data",
                    "dummy.txt"),
                AltDescription = altDescription
            },
            CategoryId = categoryId
        };

        itemRepository.Setup(x => x.AddAsync(It.IsAny<Item>()))
            .ReturnsAsync((Item item)=>item);

        categoryRepository.Setup(x => x.ExistsAsync(categoryId))
            .ReturnsAsync(true);

        fileService.Setup(x => x.UploadFileAsync(It.IsAny<IFormFile>()))
            .ReturnsAsync("https://www.example.com/image.jpg");
        // Act
        var result = await itemService.CreateItemAsync(itemVm);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        itemRepository.Verify(x => x.AddAsync(It.IsAny<Item>()), Times.Once);

        categoryRepository.Verify(x => x.ExistsAsync(categoryId), Times.AtLeastOnce);
        productRepository.Verify(x => x.GetNextOrderNumberAsync(), Times.Never);
        fileService.Verify(x => x.UploadFileAsync(It.IsAny<IFormFile>()), Times.Once);
    }

    [Fact]
    public async Task Create_CreateItemAsync_WhenCategoryDoesNotExist_ReturnsError()
    {
        var name = "Test item";
        var price = 15.99m;
        var altDescription = "Test image";
        var categoryId = 1;

        // Arrange
        var itemVm = new ItemVm
        {
            Name = name,
            Price = price,
            CategoryId = categoryId,
            Image = new ImageVm
            {
                FormFile = new FormFile(
                    new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")),
                    0,
                    0,
                    "Data",
                    "dummy.txt"),
                AltDescription = altDescription
            }
        };

        itemRepository.Setup(x => x.AddAsync(It.IsAny<Item>()))
            .ReturnsAsync((Item item)=>item);

        categoryRepository.Setup(x => x.ExistsAsync(categoryId))
            .ReturnsAsync(false);

        // Act
        var result = await itemService.CreateItemAsync(itemVm);

        // Assert
        result.IsFailure.ShouldBeTrue();
        result.Error.Code.ShouldBe(ErrorCodes.Categories.DOES_NOT_EXIST);

        itemRepository.Verify(x => x.AddAsync(It.IsAny<Item>()), Times.Never);
        categoryRepository.Verify(x => x.ExistsAsync(categoryId), Times.AtLeastOnce);
        fileService.Verify(x => x.UploadFileAsync(It.IsAny<IFormFile>()), Times.Never);
    }

    [Fact]
    public async Task Create_CreateItemAsync_OnFileServiceError_ReturnsError()
    {
        // Arrange
        var name = "Test item";
        var price = 15.99m;
        var altDescription = "Test image";
        var categoryId = 1;

        var itemVm = new ItemVm
        {
            Name = name,
            Price = price,
            CategoryId = categoryId,
            Image = new ImageVm
            {
                FormFile = new FormFile(
                    new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")),
                    0,
                    0,
                    "Data",
                    "dummy.txt"),
                AltDescription = altDescription
            }
        };

        itemRepository.Setup(x => x.AddAsync(It.IsAny<Item>()))
            .ReturnsAsync((Item item)=>item);
        categoryRepository.Setup(x => x.ExistsAsync(categoryId))
            .ReturnsAsync(true);
        fileService.Setup(x => x.UploadFileAsync(It.IsAny<IFormFile>()))
            .ThrowsAsync(new IOException());

        // Act
        var result = await itemService.CreateItemAsync(itemVm);

        // Assert
        result.IsFailure.ShouldBeTrue();
        result.Error.Code.ShouldBe(ErrorCodes.Files.FILE_UPLOAD_FAILED);

        itemRepository.Verify(x => x.AddAsync(It.IsAny<Item>()), Times.Never);
        categoryRepository.Verify(x => x.ExistsAsync(categoryId), Times.AtLeastOnce);
        fileService.Verify(x => x.UploadFileAsync(It.IsAny<IFormFile>()), Times.Once);
    }

    [Fact]
    public async Task Delete_DeleteItemAsync_WhenItemDoesNotExist_ReturnsError()
    {
        // Arrange
        var itemId = 1;

        itemRepository.Setup(x => x.GetByIdAsync(itemId))
            .ReturnsAsync((Item)null);
        itemRepository.Setup(x => x.ExistsAsync(itemId))
            .ReturnsAsync(false);

        fileService.Setup(x => x.DeleteImage(It.IsAny<string>()));

        // Act
        var result = await itemService.DeleteItemAsync(itemId);

        // Assert
        result.IsFailure.ShouldBeTrue();
        result.Error.Code.ShouldBe(ErrorCodes.Items.DOES_NOT_EXIST);

        itemRepository.Verify(x => x.DeleteAsync(itemId), Times.Never);
        productRepository.Verify(x => x.BulkUpdateOrderAsync(It.IsAny<uint>()), Times.Never);

        fileService.Verify(x => x.DeleteImage(It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public async Task Delete_DeleteItemAsync_WhenItemIsPartOfKit_ReturnsError()
    {
        // Arrange
        var itemId = 1;
        uint orderNo = 2;

        var item = new Item
        {
            Id = itemId,
            ShouldBeShown = true,
            OrderNo = orderNo
        };

        itemRepository.Setup(x => x.GetByIdAsync(itemId))
            .ReturnsAsync(item);
        itemRepository.Setup(x => x.ExistsAsync(itemId))
            .ReturnsAsync(true);
        itemRepository.Setup(x => x.IsItemAPartOfAnyKitAsync(itemId))
            .ReturnsAsync(true);

        // Act
        var result = await itemService.DeleteItemAsync(itemId);

        // Assert
        result.IsFailure.ShouldBeTrue();
        result.Error.Code.ShouldBe(ErrorCodes.Items.IS_PART_OF_KIT);

        itemRepository.Verify(x => x.DeleteAsync(itemId), Times.Never);
        itemRepository.Verify(x => x.IsItemAPartOfAnyKitAsync(itemId), Times.Once);
        
        productRepository.Verify(x => x.BulkUpdateOrderAsync(orderNo), Times.Never);

        fileService.Verify(x => x.DeleteImage(It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public async Task Delete_DeleteItemAsync_ReturnsSuccess_ReorderOtherProducts()
    {
        // Arrange
        var itemId = 1;
        uint orderNo = 2;
        var altDescription = "Test image";
        var link = "https://www.example.com/image.jpg";

        var item = new Item
        {
            Id = itemId,
            ShouldBeShown = true,
            OrderNo = orderNo,
            Image = new Image
            {
                FileUrl = link,
                AltDescription = altDescription
            }
        };

        itemRepository.Setup(x => x.GetByIdAsync(itemId))
            .ReturnsAsync(item);
        itemRepository.Setup(x => x.ExistsAsync(itemId))
            .ReturnsAsync(true);
        itemRepository.Setup(x => x.IsItemAPartOfAnyKitAsync(itemId))
            .ReturnsAsync(false);

        // Act
        var result = await itemService.DeleteItemAsync(itemId);

        // Assert
        result.IsSuccess.ShouldBeTrue();

        itemRepository.Verify(x => x.DeleteAsync(itemId), Times.Once);
        itemRepository.Verify(x => x.IsItemAPartOfAnyKitAsync(itemId), Times.Once);

        fileService.Verify(x => x.DeleteImage(It.IsAny<string>()), Times.Once);
        
        productRepository.Verify(x => x.BulkUpdateOrderAsync(orderNo), Times.Once);
    }

    [Fact]
    public async Task Delete_DeleteItemAsync_ReturnsSuccess_DoesNotReorderOtherProducts()
    {
        // Arrange
        var itemId = 1;
        uint orderNo = 2;
        var altDescription = "Test image";
        var link = "https://www.example.com/image.jpg";

        var item = new Item
        {
            Id = itemId,
            ShouldBeShown = false,
            OrderNo = orderNo,
            Image = new Image
            {
                FileUrl = link,
                AltDescription = altDescription
            }
        };

        itemRepository.Setup(x => x.GetByIdAsync(itemId))
            .ReturnsAsync(item);
        itemRepository.Setup(x => x.ExistsAsync(itemId))
            .ReturnsAsync(true);
        itemRepository.Setup(x => x.IsItemAPartOfAnyKitAsync(itemId))
            .ReturnsAsync(false);

        // Act
        var result = await itemService.DeleteItemAsync(itemId);

        // Assert
        result.IsSuccess.ShouldBeTrue();

        itemRepository.Verify(x => x.DeleteAsync(itemId), Times.Once);
        itemRepository.Verify(x => x.IsItemAPartOfAnyKitAsync(itemId), Times.Once);

        fileService.Verify(x => x.DeleteImage(It.IsAny<string>()), Times.Once);
        
        productRepository.Verify(x => x.BulkUpdateOrderAsync(orderNo), Times.Never);
    }

    [Fact]
    public async Task Edit_GetForEditAsync_OnNonExistingItem_ReturnFailure()
    {
        // Arrange
        var itemId = 1;

        var itemRepository = new Mock<IItemRepository>();
        itemRepository.Setup(x => x.GetByIdAsync(itemId))
            .ReturnsAsync((Item)null);
        itemRepository.Setup(x => x.ExistsAsync(itemId))
            .ReturnsAsync(false);

        // Act
        var result = await itemService.GetForEditAsync(itemId);

        // Assert
        result.IsFailure.ShouldBeTrue();
        result.Error.Code.ShouldBe(ErrorCodes.Items.DOES_NOT_EXIST);
    }

    [Fact]
    public async Task Update_UpdateItemAsync_WhenItemDoesNotExist_ReturnsError()
    {
        // Arrange
        var itemId = 1;
        var categoryId = 1;

        var itemVm = new ItemVm
        {
            Id = itemId,
            CategoryId = categoryId
        };

        itemRepository.Setup(x => x.GetByIdAsync(itemId))
            .ReturnsAsync((Item)null);

        categoryRepository.Setup(x => x.ExistsAsync(categoryId))
            .ReturnsAsync(true);

        // Act
        var result = await itemService.UpdateItemAsync(itemVm);

        // Assert
        result.IsFailure.ShouldBeTrue();
        result.Error.Code.ShouldBe(ErrorCodes.Items.DOES_NOT_EXIST);

        itemRepository.Verify(x => x.UpdateAsync(It.IsAny<Item>()), Times.Never);

        fileService.Verify(x => x.DeleteImage(It.IsAny<string>()), Times.Never);
        fileService.Verify(x => x.UploadFileAsync(It.IsAny<IFormFile>()), Times.Never);
    }

    [Fact]
    public async Task Update_UpdateItemAsync_WhenCategoryDoesNotExist_ReturnsError()
    {
        // Arrange
        var itemId = 1;
        var categoryId = 1;

        var itemVm = new ItemVm
        {
            Id = itemId,
            CategoryId = categoryId
        };

        var item = new Item
        {
            Id = itemId,
            CategoryId = categoryId
        };

        itemRepository.Setup(x => x.GetByIdAsync(itemId))
            .ReturnsAsync(item);

        categoryRepository.Setup(x => x.ExistsAsync(categoryId))
            .ReturnsAsync(false);

        // Act
        var result = await itemService.UpdateItemAsync(itemVm);

        // Assert
        result.IsFailure.ShouldBeTrue();
        result.Error.Code.ShouldBe(ErrorCodes.Categories.DOES_NOT_EXIST);

        itemRepository.Verify(x => x.UpdateAsync(item), Times.Never);

        fileService.Verify(x => x.DeleteImage(It.IsAny<string>()), Times.Never);
        fileService.Verify(x => x.UploadFileAsync(It.IsAny<IFormFile>()), Times.Never);
    }

    [Fact]
    public async Task Update_UpdateItemAsync_ImageNotChanged_VisibilityTrueToFalse_ReturnsSuccess()
    {
        // Arrange
        const int itemId = 1;
        const int originalCategoryId = 2;
        const int updatedCategoryId = 1;
        const string originalName = "OriginalName";
        const string updatedName = "UpdatedName";
        const string imageUrl = "https://www.example.com/image.jpg";
        const string altDescription1 = "Test image";
        const string altDescription2 = "Test image2";
        const uint orderNo = 1;

        var itemVm = new ItemVm
        {
            Id = itemId,
            Name = updatedName,
            CategoryId = updatedCategoryId,
            Image = new ImageVm
            {
                FileUrl = imageUrl,
                AltDescription = altDescription2
            },
            ShouldBeShown = false
        };

        var item = new Item
        {
            Id = itemId,
            Name = originalName,
            CategoryId = originalCategoryId,
            Image = new Image
            {
                FileUrl = imageUrl,
                AltDescription = altDescription1
            },
            ShouldBeShown = true,
            OrderNo = orderNo
        };

        itemRepository.Setup(x => x.GetByIdAsync(itemId))
            .ReturnsAsync(item);
        itemRepository.Setup(x => x.ExistsAsync(itemId))
            .ReturnsAsync(true);
        itemRepository.Setup(x => x.UpdateAsync(item))
            .ReturnsAsync((Item updatedItem) => updatedItem);

        categoryRepository.Setup(x => x.ExistsAsync(itemId))
            .ReturnsAsync(true);

        // Act
        var result = await itemService.UpdateItemAsync(itemVm);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldBeEquivalentTo(itemVm);

        itemRepository.Verify(x => x.UpdateAsync(item), Times.Once);
        
        productRepository.Verify(x => x.BulkUpdateOrderAsync(orderNo), Times.AtLeastOnce);
        productRepository.Verify(x => x.GetNextOrderNumberAsync(), Times.Never);

        fileService.Verify(x => x.DeleteImage(It.IsAny<string>()), Times.Never);
        fileService.Verify(x => x.UploadFileAsync(It.IsAny<IFormFile>()), Times.Never);
    }

    [Fact]
    public async Task Update_UpdateItemAsync_ImageNotChanged_VisibilityFalseToTrue_ReturnsSuccess()
    {
        // Arrange
        const int itemId = 1;
        const int originalCategoryId = 2;
        const int updatedCategoryId = 1;
        const string originalName = "OriginalName";
        const string updatedName = "UpdatedName";
        const string imageUrl1 = "https://www.example.com/image1.jpg";
        const string imageUrl2 = "https://www.example.com/image2.jpg";
        const string altDescription1 = "Test image";
        const string altDescription2 = "Test image2";

        var itemVm = new ItemVm
        {
            Id = itemId,
            Name = updatedName,
            CategoryId = updatedCategoryId,
            Image = new ImageVm
            {
                FileUrl = imageUrl2,
                AltDescription = altDescription2
            },
            ShouldBeShown = true
        };

        var item = new Item
        {
            Id = itemId,
            Name = originalName,
            CategoryId = originalCategoryId,
            Image = new Image
            {
                FileUrl = imageUrl1,
                AltDescription = altDescription1
            },
            ShouldBeShown = false
        };

        itemRepository.Setup(x => x.GetByIdAsync(itemId))
            .ReturnsAsync(item);
        itemRepository.Setup(x => x.ExistsAsync(itemId))
            .ReturnsAsync(true);
        itemRepository.Setup(x => x.UpdateAsync(item))
            .ReturnsAsync((Item updatedItem) => updatedItem);

        categoryRepository.Setup(x => x.ExistsAsync(itemId))
            .ReturnsAsync(true);

        // Act
        var result = await itemService.UpdateItemAsync(itemVm);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldBeEquivalentTo(itemVm with
        {
            Image = new ImageVm
            {
                FileUrl = imageUrl1,
                AltDescription = altDescription2
            }
        });

        itemRepository.Verify(x => x.UpdateAsync(item), Times.Once);
        
        productRepository.Verify(x => x.BulkUpdateOrderAsync(It.IsAny<uint>()), Times.Never);
        productRepository.Verify(x => x.GetNextOrderNumberAsync(), Times.AtLeastOnce);

        fileService.Verify(x => x.DeleteImage(It.IsAny<string>()), Times.Never);
        fileService.Verify(x => x.UploadFileAsync(It.IsAny<IFormFile>()), Times.Never);
    }

    [Fact]
    public async Task Update_UpdateItemAsync_ImageChanged_VisibilityNotChanged_ReturnsSuccess()
    {
        // Arrange
        const int itemId = 1;
        const int originalCategoryId = 2;
        const int updatedCategoryId = 1;
        const string originalName = "OriginalName";
        const string updatedName = "UpdatedName";
        const string imageUrl1 = "https://www.example.com/image1.jpg";
        const string imageUrl2 = "https://www.example.com/image2.jpg";
        const string altDescription1 = "Test image";
        const string altDescription2 = "Test image2";

        var itemVm = new ItemVm
        {
            Id = itemId,
            Name = updatedName,
            CategoryId = updatedCategoryId,
            Image = new ImageVm
            {
                FormFile = new FormFile(
                    new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")),
                    0,
                    0,
                    "Data",
                    "dummy.txt"),
                FileUrl = imageUrl1,
                AltDescription = altDescription2
            },
            ShouldBeShown = false
        };

        var item = new Item
        {
            Id = itemId,
            Name = originalName,
            CategoryId = originalCategoryId,
            Image = new Image
            {
                FileUrl = imageUrl1,
                AltDescription = altDescription1
            },
            ShouldBeShown = false
        };

        itemRepository.Setup(x => x.GetByIdAsync(itemId))
            .ReturnsAsync(item);
        itemRepository.Setup(x => x.ExistsAsync(itemId))
            .ReturnsAsync(true);
        itemRepository.Setup(x => x.UpdateAsync(It.IsAny<Item>()))
            .ReturnsAsync((Item updatedItem) => updatedItem);

        categoryRepository.Setup(x => x.ExistsAsync(itemId))
            .ReturnsAsync(true);

        fileService.Setup(x => x.DeleteImage(It.IsAny<string>()));
        fileService.Setup(x => x.UploadFileAsync(It.IsAny<IFormFile>()))
            .ReturnsAsync(imageUrl2);

        // Act
        var result = await itemService.UpdateItemAsync(itemVm);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldBeEquivalentTo(new ItemVm()
        {
            Id = itemId,
            Name = updatedName,
            CategoryId = updatedCategoryId,
            Image = new ImageVm
            {
                FileUrl = imageUrl2,
                AltDescription = altDescription2
            },
            ShouldBeShown = false
        });

        itemRepository.Verify(x => x.UpdateAsync(item), Times.Once);
        
        productRepository.Verify(x => x.BulkUpdateOrderAsync(It.IsAny<uint>()), Times.Never);
        productRepository.Verify(x => x.GetNextOrderNumberAsync(), Times.Never);

        fileService.Verify(x => x.DeleteImage(It.IsAny<string>()), Times.Once);
        fileService.Verify(x => x.UploadFileAsync(It.IsAny<IFormFile>()), Times.Once);
    }

    [Fact]
    public async Task Update_UpdateItemAsync_ImageChanged_VisibilityTrueToFalse_ReturnsSuccess()
    {
        // Arrange
        const int itemId = 1;
        const int originalCategoryId = 2;
        const int updatedCategoryId = 1;
        const string originalName = "OriginalName";
        const string updatedName = "UpdatedName";
        const string imageUrl1 = "https://www.example.com/image1.jpg";
        const string imageUrl2 = "https://www.example.com/image2.jpg";
        const string altDescription1 = "Test image";
        const string altDescription2 = "Test image2";

        var itemVm = new ItemVm
        {
            Id = itemId,
            Name = updatedName,
            CategoryId = updatedCategoryId,
            Image = new ImageVm
            {
                FormFile = new FormFile(
                    new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")),
                    0,
                    0,
                    "Data",
                    "dummy.txt"),
                FileUrl = imageUrl1,
                AltDescription = altDescription2
            },
            ShouldBeShown = false
        };

        var item = new Item
        {
            Id = itemId,
            Name = originalName,
            CategoryId = originalCategoryId,
            Image = new Image
            {
                FileUrl = imageUrl1,
                AltDescription = altDescription1
            },
            ShouldBeShown = false
        };

        itemRepository.Setup(x => x.GetByIdAsync(itemId))
            .ReturnsAsync(item);
        itemRepository.Setup(x => x.ExistsAsync(itemId))
            .ReturnsAsync(true);
        itemRepository.Setup(x => x.UpdateAsync(item))
            .ReturnsAsync((Item item) => item);

        categoryRepository.Setup(x => x.ExistsAsync(itemId))
            .ReturnsAsync(true);

        fileService.Setup(x => x.DeleteImage(It.IsAny<string>()));
        fileService.Setup(x => x.UploadFileAsync(It.IsAny<IFormFile>()))
            .ReturnsAsync(imageUrl2);

        // Act
        var result = await itemService.UpdateItemAsync(itemVm);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldBeEquivalentTo(new ItemVm()
        {
            Id = itemId,
            Name = updatedName,
            CategoryId = updatedCategoryId,
            Image = new ImageVm
            {
                FileUrl = imageUrl2,
                AltDescription = altDescription2
            },
            ShouldBeShown = false
        });

        itemRepository.Verify(x => x.UpdateAsync(item), Times.Once);
        
        productRepository.Verify(x => x.BulkUpdateOrderAsync(It.IsAny<uint>()), Times.Never);
        productRepository.Verify(x => x.GetNextOrderNumberAsync(), Times.Never);

        fileService.Verify(x => x.DeleteImage(It.IsAny<string>()), Times.Once);
        fileService.Verify(x => x.UploadFileAsync(It.IsAny<IFormFile>()), Times.Once);
    }

    [Fact]
    public async Task Update_UpdateItemAsync_ImageChanged_VisibilityFalseToTrue_ReturnsSuccess()
    {
        // Arrange
        const int itemId = 1;
        const int originalCategoryId = 2;
        const int updatedCategoryId = 1;
        const string originalName = "OriginalName";
        const string updatedName = "UpdatedName";
        const string imageUrl1 = "https://www.example.com/image1.jpg";
        const string imageUrl2 = "https://www.example.com/image2.jpg";
        const string altDescription1 = "Test image";
        const string altDescription2 = "Test image2";

        var itemVm = new ItemVm
        {
            Id = itemId,
            Name = updatedName,
            CategoryId = updatedCategoryId,
            Image = new ImageVm
            {
                FormFile = new FormFile(
                    new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")),
                    0,
                    0,
                    "Data",
                    "dummy.txt"),
                FileUrl = imageUrl1,
                AltDescription = altDescription2
            },
            ShouldBeShown = true,
        };

        var item = new Item
        {
            Id = itemId,
            Name = originalName,
            CategoryId = originalCategoryId,
            Image = new Image
            {
                FileUrl = imageUrl1,
                AltDescription = altDescription1
            },
            ShouldBeShown = false
        };

        itemRepository.Setup(x => x.GetByIdAsync(itemId))
            .ReturnsAsync(item);
        itemRepository.Setup(x => x.ExistsAsync(itemId))
            .ReturnsAsync(true);
        itemRepository.Setup(x => x.UpdateAsync(item))
            .ReturnsAsync((Item item) => item);

        categoryRepository.Setup(x => x.ExistsAsync(itemId))
            .ReturnsAsync(true);

        fileService.Setup(x => x.DeleteImage(It.IsAny<string>()));
        fileService.Setup(x => x.UploadFileAsync(It.IsAny<IFormFile>()))
            .ReturnsAsync(imageUrl2);

        // Act
        var result = await itemService.UpdateItemAsync(itemVm);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldBeEquivalentTo(new ItemVm()
        {
            Id = itemId,
            Name = updatedName,
            CategoryId = updatedCategoryId,
            Image = new ImageVm
            {
                FileUrl = imageUrl2,
                AltDescription = altDescription2
            },
            ShouldBeShown = true
        });

        itemRepository.Verify(x => x.UpdateAsync(item), Times.Once);
        
        productRepository.Verify(x => x.BulkUpdateOrderAsync(It.IsAny<uint>()), Times.Never);
        productRepository.Verify(x => x.GetNextOrderNumberAsync(), Times.Once);

        fileService.Verify(x => x.DeleteImage(It.IsAny<string>()), Times.Once);
        fileService.Verify(x => x.UploadFileAsync(It.IsAny<IFormFile>()), Times.Once);
    }

    [Fact]
    public async Task Update_UpdateItemComponentsAsync_ItemDoesNotExist_ReturnsFailure()
    {
        // Arrange
        var selectionVm = new ItemComponentsSelectionVm()
        {
            Item = new ItemVm { Id = 1 },
            AllComponents = [],
            SelectedComponentsIds = []
        };
        itemRepository.Setup(repo => repo.ExistsAsync(selectionVm.Item.Id))
            .ReturnsAsync(false);

        // Act
        var result = await itemService.UpdateItemComponentsAsync(selectionVm);

        // Assert
        result.IsFailure.ShouldBeTrue();
        result.Error.Code.ShouldBe(ErrorCodes.Items.DOES_NOT_EXIST);
    }

    private Item MapItemFromVm(ItemVm itemVm)
    {
        var item = new Item
        {
            Id = itemVm.Id,
            Name = itemVm.Name,
            Image = new Image
            {
                AltDescription = itemVm.Image!.AltDescription,
                FileUrl = itemVm.Image.FileUrl
            },
            CategoryId = itemVm.CategoryId!.Value,
            ShouldBeShown = itemVm.ShouldBeShown
        };
        item.SetPrice(itemVm.Price);
        return item;
    }

    private ItemVm MapItemToVm(Item item)
    {
        return new ItemVm
        {
            Id = item.Id,
            Name = item.Name,
            Image = new ImageVm
            {
                AltDescription = item.Image!.AltDescription,
                FileUrl = item.Image.FileUrl
            },
            CategoryId = item.CategoryId,
            ShouldBeShown = item.ShouldBeShown,
            Price = item.Price
        };
    }
}