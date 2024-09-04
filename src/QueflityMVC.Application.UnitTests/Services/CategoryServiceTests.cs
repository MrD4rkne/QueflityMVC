using AutoMapper;
using Moq;
using QueflityMVC.Application.Results;
using QueflityMVC.Application.Services;
using QueflityMVC.Application.ViewModels.Category;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;
using Shouldly;

namespace QueflityMVC.Application.UnitTests.Services;

public class CategoryServiceTests
{
    [Fact]
    public async Task Add_CreateCategoryAsync_ShouldReturnSuccess()
    {
        // Arrange
        string name = "Test Category";
        var createCategoryVm = new CategoryVm
        {
            Id = 0,
            Name = name
        };

        var repositoryMock = new Mock<ICategoryRepository>();
        repositoryMock.Setup(repository => repository.AddAsync(It.IsAny<Category>()))
            .ReturnsAsync(1);

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(mapper => mapper.Map<Category>(createCategoryVm))
            .Returns(new Category()
            {
                Name = name
            });

        var categoryService = new CategoryService(repositoryMock.Object, mapperMock.Object);

        // Act
        var result = await categoryService.CreateCategoryAsync(createCategoryVm);

        // Assert
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task Add_CreateCategoryAsync_ShouldReturnDuplicatedName()
    {
        // Arrange
        string name = "Test Category";
        var createCategoryVm = new CategoryVm
        {
            Id = 0,
            Name = name
        };

        var repositoryMock = new Mock<ICategoryRepository>();
        repositoryMock.Setup(repository => repository.DoesCategoryWithNameExistAsync(createCategoryVm.Name))
            .ReturnsAsync(true);
        repositoryMock.Setup(repository => repository.AddAsync(It.IsAny<Category>()))
            .ReturnsAsync(1);

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(mapper => mapper.Map<Category>(createCategoryVm))
            .Returns(new Category()
            {
                Name = name
            });

        var categoryService = new CategoryService(repositoryMock.Object, mapperMock.Object);

        // Act
        var result = await categoryService.CreateCategoryAsync(createCategoryVm);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(Errors.Categories.DuplicatedName.Code, result.Error.Code);
    }

    [Fact]
    public async Task Delete_DeleteCategoryAsync_ShouldDelete()
    {
        // Arrange
        var id = 1;

        var repositoryMock = new Mock<ICategoryRepository>();
        repositoryMock.Setup(repository => repository.IsAnyItemWithCategory(id))
            .ReturnsAsync(false);
        repositoryMock.Setup(repository => repository.DeleteAsync(It.IsAny<Category>()))
            .Returns(Task.CompletedTask);
        repositoryMock.Setup(repository => repository.ExistsAsync(id))
            .ReturnsAsync(true);


        var mapperMock = new Mock<IMapper>();
        var categoryService = new CategoryService(repositoryMock.Object, mapperMock.Object);

        // Act
        var result = await categoryService.DeleteCategoryAsync(id);

        // Assert
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task Delete_DeleteCategoryAsync_ShouldReturnError_ItemWithCategoryExist()
    {
        // Arrange
        var id = 1;

        var repositoryMock = new Mock<ICategoryRepository>();
        repositoryMock.Setup(repository => repository.IsAnyItemWithCategory(id))
            .ReturnsAsync(true);
        repositoryMock.Setup(repository => repository.ExistsAsync(id))
            .ReturnsAsync(true);
        repositoryMock.Setup(repository => repository.DeleteAsync(It.IsAny<Category>()))
            .Throws<InvalidOperationException>();

        var mapperMock = new Mock<IMapper>();
        var categoryService = new CategoryService(repositoryMock.Object, mapperMock.Object);

        // Act
        var result = await categoryService.DeleteCategoryAsync(id);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(Errors.Categories.HasItems.Code, result.Error.Code);
    }

    [Fact]
    public async Task Delete_DeleteCategoryAsync_ShouldReturnError_DoesNotExist()
    {
        // Arrange
        var id = 1;

        var repositoryMock = new Mock<ICategoryRepository>();
        repositoryMock.Setup(repository => repository.IsAnyItemWithCategory(id))
            .ReturnsAsync(false);
        repositoryMock.Setup(repository => repository.ExistsAsync(id))
            .ReturnsAsync(false);

        var mapperMock = new Mock<IMapper>();
        var categoryService = new CategoryService(repositoryMock.Object, mapperMock.Object);

        // Act
        var result = await categoryService.DeleteCategoryAsync(id);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(Errors.Categories.DoesNotExist.Code, result.Error.Code);
    }

    [Fact]
    public async Task Get_GetVmForEditAsync_ShouldReturnCategoryVm()
    {
        // Arrange
        const int id = 1;
        var name = "Test Category";

        var repositoryMock = new Mock<ICategoryRepository>();
        repositoryMock.Setup(repository => repository.GetByIdAsync(id))
            .ReturnsAsync(new Category
            {
                Id = id,
                Name = name
            });
        repositoryMock.Setup(repository => repository.ExistsAsync(id))
            .ReturnsAsync(true);

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(mapper => mapper.Map<CategoryVm>(It.IsAny<Category>()))
            .Returns(new CategoryVm
            {
                Id = id,
                Name = name
            });

        var categoryService = new CategoryService(repositoryMock.Object, mapperMock.Object);

        // Act
        var result = await categoryService.GetVmForEditAsync(id);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(id, result.Value.Id);
        Assert.Equal(name, result.Value.Name);
    }

    [Fact]
    public async Task Get_GetVmForEditAsync_ShouldReturnError_DoesNotExist()
    {
        // Arrange
        var id = 1;

        var repositoryMock = new Mock<ICategoryRepository>();
        repositoryMock.Setup(repository => repository.GetByIdAsync(id))
            .ReturnsAsync((Category)null);
        repositoryMock.Setup(repository => repository.ExistsAsync(id))
            .ReturnsAsync(false);

        var mapperMock = new Mock<IMapper>();
        var categoryService = new CategoryService(repositoryMock.Object, mapperMock.Object);

        // Act
        var result = await categoryService.GetVmForEditAsync(id);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Equal(Errors.Categories.DoesNotExist.Code, result.Error.Code);
    }

    [Fact]
    public async Task Update_UpdateCategoryAsync_ShouldReturnSuccess()
    {
        // Arrange
        var id = 1;
        var name = "Test Category";

        var expectedCategory = new Category { Id = id, Name = name };
        var expectedCategoryVm = new CategoryVm { Id = id, Name = name };

        var updateCategoryVm = new CategoryVm
        {
            Id = id,
            Name = name
        };

        var repositoryMock = new Mock<ICategoryRepository>();
        repositoryMock.Setup(repository => repository.ExistsAsync(id))
            .ReturnsAsync(true);
        repositoryMock.Setup(repository =>
                repository.UpdateAsync(expectedCategory))
            .ReturnsAsync(expectedCategory);
        repositoryMock.Setup(repository => repository.DoesCategoryWithNameButNotIdExistAsync(id, name))
            .ReturnsAsync(false);

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(mapper => mapper.Map<Category>(updateCategoryVm))
            .Returns(expectedCategory);

        mapperMock.Setup(mapper => mapper.Map<CategoryVm>(expectedCategory))
            .Returns(expectedCategoryVm);

        var categoryService = new CategoryService(repositoryMock.Object, mapperMock.Object);

        // Act
        var result = await categoryService.UpdateCategoryAsync(updateCategoryVm);

        // Assert
        Assert.True(result.IsSuccess);
        result.Value.Id.ShouldBe(id);
        result.Value.Name.ShouldBe(name);
    }

    [Fact]
    public async Task Update_UpdateCategoryAsync_ShouldReturnError_DoesNotExist()
    {
        // Arrange
        var id = 1;
        var name = "Test Category";

        var updateCategoryVm = new CategoryVm
        {
            Id = id,
            Name = name
        };

        var repositoryMock = new Mock<ICategoryRepository>();
        repositoryMock.Setup(repository => repository.ExistsAsync(id))
            .ReturnsAsync(false);

        var mapperMock = new Mock<IMapper>();
        var categoryService = new CategoryService(repositoryMock.Object, mapperMock.Object);

        // Act
        var result = await categoryService.UpdateCategoryAsync(updateCategoryVm);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(Errors.Categories.DoesNotExist.Code, result.Error.Code);
    }

    [Fact]
    public async Task Update_UpdateCategoryAsync_ShouldReturnError_DuplicatedName()
    {
        // Arrange
        var id = 1;
        var name = "Test Category";

        var updateCategoryVm = new CategoryVm
        {
            Id = id,
            Name = name
        };

        var repositoryMock = new Mock<ICategoryRepository>();
        repositoryMock.Setup(repository => repository.ExistsAsync(id))
            .ReturnsAsync(true);
        repositoryMock.Setup(repository => repository.DoesCategoryWithNameButNotIdExistAsync(id, name))
            .ReturnsAsync(true);

        var mapperMock = new Mock<IMapper>();
        var categoryService = new CategoryService(repositoryMock.Object, mapperMock.Object);

        // Act
        var result = await categoryService.UpdateCategoryAsync(updateCategoryVm);

        // Assert
        result.IsSuccess.ShouldBeFalse();
        result.Error.Code.ShouldBe(Errors.Categories.DuplicatedName.Code);
    }
}