using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.Results;
using QueflityMVC.Application.Services;
using QueflityMVC.Application.ViewModels.Image;
using QueflityMVC.Application.ViewModels.Item;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;
using Shouldly;

namespace QueflityMVC.Application.UnitTests.Services;

public class ItemServiceTests
{
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

        var item = new Item
        {
            Name = name,
            Image = new Image
            {
                AltDescription = altDescription
            },
            ShouldBeShown = true,
            CategoryId = categoryId
        };
        item.SetPrice(price);

        var itemRepository = new Mock<IItemRepository>();
        itemRepository.Setup(x => x.AddAsync(item))
            .ReturnsAsync(1);

        var mapper = new Mock<IMapper>();
        mapper.Setup(x => x.Map<Item>(itemVm))
            .Returns(item);
        mapper.Setup(x => x.Map<ItemVm>(item))
            .Returns(itemVm);

        var categoryRepository = new Mock<ICategoryRepository>();
        categoryRepository.Setup(x => x.ExistsAsync(categoryId))
            .ReturnsAsync(true);

        var componentRepository = new Mock<IComponentRepository>();

        var fileService = new Mock<IFileService>();
        fileService.Setup(x => x.UploadFileAsync(It.IsAny<IFormFile>()))
            .ReturnsAsync("https://www.example.com/image.jpg");

        var purchasableRepository = new Mock<IProductRepository>();
        purchasableRepository.Setup(x => x.GetNextOrderNumberAsync())
            .ReturnsAsync((uint)2);

        var logger = new Mock<ILogger<ItemService>>();

        var itemService = new ItemService(
            itemRepository.Object,
            mapper.Object,
            categoryRepository.Object,
            componentRepository.Object,
            fileService.Object,
            purchasableRepository.Object,
            logger.Object);

        // Act
        var result = await itemService.CreateItemAsync(itemVm);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        itemRepository.Verify(x => x.AddAsync(item), Times.Once);
        categoryRepository.Verify(x => x.ExistsAsync(categoryId), Times.AtLeastOnce);
        purchasableRepository.Verify(x => x.GetNextOrderNumberAsync(), Times.Once);
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

        var item = new Item
        {
            Name = name,
            Image = new Image
            {
                AltDescription = altDescription
            },
            CategoryId = categoryId
        };
        item.SetPrice(price);

        var itemRepository = new Mock<IItemRepository>();
        itemRepository.Setup(x => x.AddAsync(item))
            .ReturnsAsync(1);

        var mapper = new Mock<IMapper>();
        mapper.Setup(x => x.Map<Item>(itemVm))
            .Returns(item);
        mapper.Setup(x => x.Map<ItemVm>(item))
            .Returns(itemVm);

        var categoryRepository = new Mock<ICategoryRepository>();
        categoryRepository.Setup(x => x.ExistsAsync(categoryId))
            .ReturnsAsync(true);

        var componentRepository = new Mock<IComponentRepository>();

        var fileService = new Mock<IFileService>();
        fileService.Setup(x => x.UploadFileAsync(It.IsAny<IFormFile>()))
            .ReturnsAsync("https://www.example.com/image.jpg");

        var purchasableRepository = new Mock<IProductRepository>();

        var logger = new Mock<ILogger<ItemService>>();

        var itemService = new ItemService(
            itemRepository.Object,
            mapper.Object,
            categoryRepository.Object,
            componentRepository.Object,
            fileService.Object,
            purchasableRepository.Object,
            logger.Object);

        // Act
        var result = await itemService.CreateItemAsync(itemVm);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        itemRepository.Verify(x => x.AddAsync(item), Times.Once);
        categoryRepository.Verify(x => x.ExistsAsync(categoryId), Times.AtLeastOnce);
        purchasableRepository.Verify(x => x.GetNextOrderNumberAsync(), Times.Never);
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

        var item = new Item
        {
            Name = name,
            Image = new Image
            {
                AltDescription = altDescription
            },
            CategoryId = categoryId
        };
        item.SetPrice(price);

        var itemRepository = new Mock<IItemRepository>();
        itemRepository.Setup(x => x.AddAsync(item))
            .ReturnsAsync(1);

        var mapper = new Mock<IMapper>();
        var categoryRepository = new Mock<ICategoryRepository>();
        categoryRepository.Setup(x => x.ExistsAsync(categoryId))
            .ReturnsAsync(false);

        var componentRepository = new Mock<IComponentRepository>();

        var fileService = new Mock<IFileService>();
        fileService.Setup(x => x.UploadFileAsync(It.IsAny<IFormFile>()))
            .ReturnsAsync("https://www.example.com/image.jpg");

        var purchasableRepository = new Mock<IProductRepository>();
        var logger = new Mock<ILogger<ItemService>>();

        var itemService = new ItemService(
            itemRepository.Object,
            mapper.Object,
            categoryRepository.Object,
            componentRepository.Object,
            fileService.Object,
            purchasableRepository.Object,
            logger.Object);

        // Act
        var result = await itemService.CreateItemAsync(itemVm);

        // Assert
        result.IsFailure.ShouldBeTrue();
        result.Error.Code.ShouldBe(ErrorCodes.Categories.DOES_NOT_EXIST);
        itemRepository.Verify(x => x.AddAsync(item), Times.Never);
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

        var item = new Item
        {
            Name = name,
            Image = new Image
            {
                AltDescription = altDescription
            },
            CategoryId = categoryId
        };
        item.SetPrice(price);

        var itemRepository = new Mock<IItemRepository>();
        itemRepository.Setup(x => x.AddAsync(item))
            .ReturnsAsync(1);

        var mapper = new Mock<IMapper>();
        var categoryRepository = new Mock<ICategoryRepository>();
        categoryRepository.Setup(x => x.ExistsAsync(categoryId))
            .ReturnsAsync(true);

        var componentRepository = new Mock<IComponentRepository>();

        var fileService = new Mock<IFileService>();
        fileService.Setup(x => x.UploadFileAsync(It.IsAny<IFormFile>()))
            .ThrowsAsync(new IOException());

        var purchasableRepository = new Mock<IProductRepository>();
        var logger = new Mock<ILogger<ItemService>>();

        var itemService = new ItemService(
            itemRepository.Object,
            mapper.Object,
            categoryRepository.Object,
            componentRepository.Object,
            fileService.Object,
            purchasableRepository.Object,
            logger.Object);

        // Act
        var result = await itemService.CreateItemAsync(itemVm);

        // Assert
        result.IsFailure.ShouldBeTrue();
        result.Error.Code.ShouldBe(ErrorCodes.Files.FILE_UPLOAD_FAILED);
        itemRepository.Verify(x => x.AddAsync(item), Times.Never);
        categoryRepository.Verify(x => x.ExistsAsync(categoryId), Times.AtLeastOnce);
        fileService.Verify(x => x.UploadFileAsync(It.IsAny<IFormFile>()), Times.Once);
    }

    [Fact]
    public async Task Delete_DeleteItemAsync_WhenItemDoesNotExist_ReturnsError()
    {
        // Arrange
        var itemId = 1;

        var itemRepository = new Mock<IItemRepository>();
        itemRepository.Setup(x => x.GetByIdAsync(itemId))
            .ReturnsAsync((Item)null);
        itemRepository.Setup(x => x.ExistsAsync(itemId))
            .ReturnsAsync(false);

        var mapper = new Mock<IMapper>();
        var categoryRepository = new Mock<ICategoryRepository>();
        var componentRepository = new Mock<IComponentRepository>();

        var fileService = new Mock<IFileService>();
        fileService.Setup(x => x.DeleteImage(It.IsAny<string>()));

        var purchasableRepository = new Mock<IProductRepository>();
        var logger = new Mock<ILogger<ItemService>>();

        var itemService = new ItemService(
            itemRepository.Object,
            mapper.Object,
            categoryRepository.Object,
            componentRepository.Object,
            fileService.Object,
            purchasableRepository.Object,
            logger.Object);

        // Act
        var result = await itemService.DeleteItemAsync(itemId);

        // Assert
        result.IsFailure.ShouldBeTrue();
        result.Error.Code.ShouldBe(ErrorCodes.Items.DOES_NOT_EXIST);
        itemRepository.Verify(x => x.DeleteAsync(itemId), Times.Never);
        itemRepository.Verify(x => x.BulkUpdateOrderAsync(It.IsAny<uint>()), Times.Never);
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

        var itemRepository = new Mock<IItemRepository>();
        itemRepository.Setup(x => x.GetByIdAsync(itemId))
            .ReturnsAsync(item);
        itemRepository.Setup(x => x.ExistsAsync(itemId))
            .ReturnsAsync(true);
        itemRepository.Setup(x => x.IsItemAPartOfAnyKitAsync(itemId))
            .ReturnsAsync(true);

        var mapper = new Mock<IMapper>();
        var categoryRepository = new Mock<ICategoryRepository>();
        var componentRepository = new Mock<IComponentRepository>();
        var fileService = new Mock<IFileService>();
        var purchasableRepository = new Mock<IProductRepository>();
        var logger = new Mock<ILogger<ItemService>>();

        var itemService = new ItemService(
            itemRepository.Object,
            mapper.Object,
            categoryRepository.Object,
            componentRepository.Object,
            fileService.Object,
            purchasableRepository.Object,
            logger.Object);

        // Act
        var result = await itemService.DeleteItemAsync(itemId);

        // Assert
        result.IsFailure.ShouldBeTrue();
        result.Error.Code.ShouldBe(ErrorCodes.Items.IS_PART_OF_KIT);
        itemRepository.Verify(x => x.DeleteAsync(itemId), Times.Never);
        itemRepository.Verify(x => x.BulkUpdateOrderAsync(orderNo), Times.Never);
        itemRepository.Verify(x => x.IsItemAPartOfAnyKitAsync(itemId), Times.Once);
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

        var itemRepository = new Mock<IItemRepository>();
        itemRepository.Setup(x => x.GetByIdAsync(itemId))
            .ReturnsAsync(item);
        itemRepository.Setup(x => x.ExistsAsync(itemId))
            .ReturnsAsync(true);
        itemRepository.Setup(x => x.IsItemAPartOfAnyKitAsync(itemId))
            .ReturnsAsync(false);

        var mapper = new Mock<IMapper>();
        var categoryRepository = new Mock<ICategoryRepository>();
        var componentRepository = new Mock<IComponentRepository>();
        var fileService = new Mock<IFileService>();
        var purchasableRepository = new Mock<IProductRepository>();
        var logger = new Mock<ILogger<ItemService>>();

        var itemService = new ItemService(
            itemRepository.Object,
            mapper.Object,
            categoryRepository.Object,
            componentRepository.Object,
            fileService.Object,
            purchasableRepository.Object,
            logger.Object);

        // Act
        var result = await itemService.DeleteItemAsync(itemId);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        itemRepository.Verify(x => x.DeleteAsync(itemId), Times.Once);
        itemRepository.Verify(x => x.BulkUpdateOrderAsync(orderNo), Times.Once);
        itemRepository.Verify(x => x.IsItemAPartOfAnyKitAsync(itemId), Times.Once);
        fileService.Verify(x => x.DeleteImage(It.IsAny<string>()), Times.Once);
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

        var itemRepository = new Mock<IItemRepository>();
        itemRepository.Setup(x => x.GetByIdAsync(itemId))
            .ReturnsAsync(item);
        itemRepository.Setup(x => x.ExistsAsync(itemId))
            .ReturnsAsync(true);
        itemRepository.Setup(x => x.IsItemAPartOfAnyKitAsync(itemId))
            .ReturnsAsync(false);

        var mapper = new Mock<IMapper>();
        var categoryRepository = new Mock<ICategoryRepository>();
        var componentRepository = new Mock<IComponentRepository>();
        var fileService = new Mock<IFileService>();
        var purchasableRepository = new Mock<IProductRepository>();
        var logger = new Mock<ILogger<ItemService>>();

        var itemService = new ItemService(
            itemRepository.Object,
            mapper.Object,
            categoryRepository.Object,
            componentRepository.Object,
            fileService.Object,
            purchasableRepository.Object,
            logger.Object);

        // Act
        var result = await itemService.DeleteItemAsync(itemId);

        // Assert
        result.IsSuccess.ShouldBeTrue();

        itemRepository.Verify(x => x.DeleteAsync(itemId), Times.Once);
        itemRepository.Verify(x => x.BulkUpdateOrderAsync(orderNo), Times.Never);
        itemRepository.Verify(x => x.IsItemAPartOfAnyKitAsync(itemId), Times.Once);
        fileService.Verify(x => x.DeleteImage(It.IsAny<string>()), Times.Once);
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

        var mapper = new Mock<IMapper>();
        var categoryRepository = new Mock<ICategoryRepository>();
        var componentRepository = new Mock<IComponentRepository>();
        var fileService = new Mock<IFileService>();
        var purchasableRepository = new Mock<IProductRepository>();
        var logger = new Mock<ILogger<ItemService>>();

        var itemService = new ItemService(
            itemRepository.Object,
            mapper.Object,
            categoryRepository.Object,
            componentRepository.Object,
            fileService.Object,
            purchasableRepository.Object,
            logger.Object);

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

        var itemRepository = new Mock<IItemRepository>();
        itemRepository.Setup(x => x.GetByIdAsync(itemId))
            .ReturnsAsync((Item)null);

        var mapper = new Mock<IMapper>();
        var categoryRepository = new Mock<ICategoryRepository>();
        categoryRepository.Setup(x => x.ExistsAsync(categoryId))
            .ReturnsAsync(true);

        var componentRepository = new Mock<IComponentRepository>();
        var fileService = new Mock<IFileService>();
        var purchasableRepository = new Mock<IProductRepository>();
        var logger = new Mock<ILogger<ItemService>>();

        var itemService = new ItemService(
            itemRepository.Object,
            mapper.Object,
            categoryRepository.Object,
            componentRepository.Object,
            fileService.Object,
            purchasableRepository.Object,
            logger.Object);

        // Act
        var result = await itemService.UpdateItemAsync(itemVm);

        // Assert
        result.IsFailure.ShouldBeTrue();
        result.Error.Code.ShouldBe(ErrorCodes.Items.DOES_NOT_EXIST);
        itemRepository.Verify(x => x.UpdateAsync(It.IsAny<Item>()), Times.Never);
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

        var itemRepository = new Mock<IItemRepository>();
        itemRepository.Setup(x => x.GetByIdAsync(itemId))
            .ReturnsAsync(item);

        var mapper = new Mock<IMapper>();
        var categoryRepository = new Mock<ICategoryRepository>();
        categoryRepository.Setup(x => x.ExistsAsync(categoryId))
            .ReturnsAsync(false);

        var componentRepository = new Mock<IComponentRepository>();
        var fileService = new Mock<IFileService>();
        var purchasableRepository = new Mock<IProductRepository>();
        var logger = new Mock<ILogger<ItemService>>();

        var itemService = new ItemService(
            itemRepository.Object,
            mapper.Object,
            categoryRepository.Object,
            componentRepository.Object,
            fileService.Object,
            purchasableRepository.Object,
            logger.Object);

        // Act
        var result = await itemService.UpdateItemAsync(itemVm);

        // Assert
        result.IsFailure.ShouldBeTrue();
        result.Error.Code.ShouldBe(ErrorCodes.Categories.DOES_NOT_EXIST);
        itemRepository.Verify(x => x.UpdateAsync(item), Times.Never);
    }

    [Fact]
    public async Task Update_UpdateItemAsync_ImageNotChanged_VisibilityNotChanged_ReturnsSuccess()
    {
        // Arrange
        var itemVm = new ItemVm
        {
            Id = 1,
            CategoryId = 1,
            Image = new ImageVm
            {
                FileUrl = "https://www.example.com/image.jpg",
                AltDescription = "Test image"
            },
            ShouldBeShown = false
        };

        var item = new Item
        {
            Id = 1,
            CategoryId = 2,
            Image = new Image
            {
                FileUrl = "https://www.example.com/image.jpg",
                AltDescription = "Test image2"
            },
            ShouldBeShown = false
        };

        var itemRepository = new Mock<IItemRepository>();
        itemRepository.Setup(x => x.GetByIdAsync(itemVm.Id))
            .ReturnsAsync(item);
        itemRepository.Setup(x => x.ExistsAsync(itemVm.Id))
            .ReturnsAsync(true);
        itemRepository.Setup(x => x.UpdateAsync(item))
            .ReturnsAsync((Item item) => item);

        var mapper = new Mock<IMapper>();
        mapper.Setup(x => x.Map<Item>(It.IsAny<ItemVm>()))
            .Returns((ItemVm itVm) => MapItemFromVm(itVm));
        mapper.Setup(x => x.Map<ItemVm>(It.IsAny<Item>()))
            .Returns((Item it) => MapItemToVm(it));

        var categoryRepository = new Mock<ICategoryRepository>();
        categoryRepository.Setup(x => x.ExistsAsync(itemVm.Id))
            .ReturnsAsync(true);

        var componentRepository = new Mock<IComponentRepository>();
        var fileService = new Mock<IFileService>();
        var purchasableRepository = new Mock<IProductRepository>();
        var logger = new Mock<ILogger<ItemService>>();

        var itemService = new ItemService(
            itemRepository.Object,
            mapper.Object,
            categoryRepository.Object,
            componentRepository.Object,
            fileService.Object,
            purchasableRepository.Object,
            logger.Object);

        // Act
        var result = await itemService.UpdateItemAsync(itemVm);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldBeEquivalentTo(itemVm);
        itemRepository.Verify(x => x.UpdateAsync(item), Times.Once);
        itemRepository.Verify(x => x.BulkUpdateOrderAsync(It.IsAny<uint>()), Times.Never);
        purchasableRepository.Verify(x => x.GetNextOrderNumberAsync(), Times.Never);
    }

    [Fact]
    public async Task Update_UpdateItemAsync_ImageNotChanged_VisibilityTrueToFalse_ReturnsSuccess()
    {
        // Arrange
        uint orderNo = 1;

        var itemVm = new ItemVm
        {
            Id = 1,
            CategoryId = 1,
            Image = new ImageVm
            {
                FileUrl = "https://www.example.com/image.jpg",
                AltDescription = "Test image"
            },
            ShouldBeShown = false
        };

        var item = new Item
        {
            Id = 1,
            CategoryId = 2,
            Image = new Image
            {
                FileUrl = "https://www.example.com/image.jpg",
                AltDescription = "Test image2"
            },
            ShouldBeShown = true,
            OrderNo = orderNo
        };

        var itemRepository = new Mock<IItemRepository>();
        itemRepository.Setup(x => x.GetByIdAsync(itemVm.Id))
            .ReturnsAsync(item);
        itemRepository.Setup(x => x.ExistsAsync(itemVm.Id))
            .ReturnsAsync(true);
        itemRepository.Setup(x => x.UpdateAsync(item))
            .ReturnsAsync((Item item) => new Item
            {
                Id = item.Id,
                CategoryId = item.CategoryId,
                Image = item.Image,
                ShouldBeShown = item.ShouldBeShown,
                OrderNo = item.OrderNo
            });

        var mapper = new Mock<IMapper>();
        mapper.Setup(x => x.Map<Item>(It.IsAny<ItemVm>()))
            .Returns((ItemVm itemVm) => MapItemFromVm(itemVm));
        mapper.Setup(x => x.Map<ItemVm>(It.IsAny<Item>()))
            .Returns((Item item) => MapItemToVm(item));

        var categoryRepository = new Mock<ICategoryRepository>();
        categoryRepository.Setup(x => x.ExistsAsync(itemVm.Id))
            .ReturnsAsync(true);

        var componentRepository = new Mock<IComponentRepository>();
        var fileService = new Mock<IFileService>();
        var purchasableRepository = new Mock<IProductRepository>();
        var logger = new Mock<ILogger<ItemService>>();

        var itemService = new ItemService(
            itemRepository.Object,
            mapper.Object,
            categoryRepository.Object,
            componentRepository.Object,
            fileService.Object,
            purchasableRepository.Object,
            logger.Object);

        // Act
        var result = await itemService.UpdateItemAsync(itemVm);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldBeEquivalentTo(itemVm);
        itemRepository.Verify(x => x.UpdateAsync(item), Times.Once);
        itemRepository.Verify(x => x.BulkUpdateOrderAsync(orderNo), Times.AtLeastOnce);
        purchasableRepository.Verify(x => x.GetNextOrderNumberAsync(), Times.Never);
    }

    [Fact]
    public async Task Update_UpdateItemAsync_ImageNotChanged_VisibilityFalseToTrue_ReturnsSuccess()
    {
        // Arrange
        var itemVm = new ItemVm
        {
            Id = 1,
            CategoryId = 1,
            Image = new ImageVm
            {
                FileUrl = "https://www.example.com/image.jpg",
                AltDescription = "Test image"
            },
            ShouldBeShown = true
        };

        var item = new Item
        {
            Id = 1,
            CategoryId = 2,
            Image = new Image
            {
                FileUrl = "https://www.example.com/image.jpg",
                AltDescription = "Test image2"
            },
            ShouldBeShown = false
        };

        var itemRepository = new Mock<IItemRepository>();
        itemRepository.Setup(x => x.GetByIdAsync(itemVm.Id))
            .ReturnsAsync(item);
        itemRepository.Setup(x => x.ExistsAsync(itemVm.Id))
            .ReturnsAsync(true);
        itemRepository.Setup(x => x.UpdateAsync(item))
            .ReturnsAsync((Item item) => new Item
            {
                Id = item.Id,
                CategoryId = item.CategoryId,
                Image = item.Image,
                ShouldBeShown = item.ShouldBeShown,
                OrderNo = item.OrderNo
            });

        var mapper = new Mock<IMapper>();
        mapper.Setup(x => x.Map<Item>(It.IsAny<ItemVm>()))
            .Returns((ItemVm itemVm) => MapItemFromVm(itemVm));
        mapper.Setup(x => x.Map<ItemVm>(It.IsAny<Item>()))
            .Returns((Item item) => MapItemToVm(item));

        var categoryRepository = new Mock<ICategoryRepository>();
        categoryRepository.Setup(x => x.ExistsAsync(itemVm.Id))
            .ReturnsAsync(true);

        var componentRepository = new Mock<IComponentRepository>();
        var fileService = new Mock<IFileService>();
        var purchasableRepository = new Mock<IProductRepository>();
        var logger = new Mock<ILogger<ItemService>>();

        var itemService = new ItemService(
            itemRepository.Object,
            mapper.Object,
            categoryRepository.Object,
            componentRepository.Object,
            fileService.Object,
            purchasableRepository.Object,
            logger.Object);

        // Act
        var result = await itemService.UpdateItemAsync(itemVm);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldBeEquivalentTo(itemVm);
        itemRepository.Verify(x => x.UpdateAsync(item), Times.Once);
        itemRepository.Verify(x => x.BulkUpdateOrderAsync(It.IsAny<uint>()), Times.Never);
        purchasableRepository.Verify(x => x.GetNextOrderNumberAsync(), Times.AtLeastOnce);
    }

    // TODO: Add more tests for when image is changed

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