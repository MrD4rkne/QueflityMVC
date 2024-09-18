using AutoMapper;
using Moq;
using QueflityMVC.Application.Results;
using QueflityMVC.Application.Services;
using QueflityMVC.Application.ViewModels.Component;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;
using Shouldly;

namespace QueflityMVC.Application.UnitTests.Services;

public class ComponentServiceTests
{
    [Fact]
    public async Task Create_CreateComponentAsync_WithDuplicatedName_ReturnsFailureResult()
    {
        // Arrange
        var id = 1;
        var name = "ComponentName";

        var mapperMock = new Mock<IMapper>();
        var componentToCreateVm = new ComponentVm { Id = id, Name = name };

        var componentRepositoryMock = new Mock<IComponentRepository>();
        componentRepositoryMock
            .Setup(repository => repository.DoesComponentWithNameExistAsync(componentToCreateVm.Name))
            .ReturnsAsync(true);

        var componentService = new ComponentService(componentRepositoryMock.Object, mapperMock.Object);

        // Act
        var result = await componentService.CreateComponentAsync(componentToCreateVm);

        // Assert
        result.IsFailure.ShouldBeTrue();
        result.Error.Code.ShouldBe(Errors.Components.DuplicatedName.Code);

        componentRepositoryMock.Verify(repository => repository.AddAsync(It.IsAny<Component>()), Times.Never);
    }

    [Fact]
    public async Task Create_CreateComponentAsync_WithUniqueName_ReturnsSuccessResult()
    {
        // Arrange
        var id = 1;
        var name = "ComponentName";

        var mapperMock = new Mock<IMapper>();
        var componentToCreateVm = new ComponentVm { Id = id, Name = name };

        var componentRepositoryMock = new Mock<IComponentRepository>();
        componentRepositoryMock
            .Setup(repository => repository.DoesComponentWithNameExistAsync(componentToCreateVm.Name))
            .ReturnsAsync(false);

        var componentService = new ComponentService(componentRepositoryMock.Object, mapperMock.Object);

        // Act
        var result = await componentService.CreateComponentAsync(componentToCreateVm);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        componentRepositoryMock.Verify(repository => repository.AddAsync(It.IsAny<Component>()), Times.Once);
    }

    [Fact]
    public async Task Delete_DeleteComponentAsync_WithNonExistingComponent_ReturnsFailureResult()
    {
        // Arrange
        var id = 1;

        var mapperMock = new Mock<IMapper>();
        var componentRepositoryMock = new Mock<IComponentRepository>();
        componentRepositoryMock.Setup(repository => repository.ExistsAsync(id))
            .ReturnsAsync(false);

        var componentService = new ComponentService(componentRepositoryMock.Object, mapperMock.Object);

        // Act
        var result = await componentService.DeleteComponentAsync(id);

        // Assert
        result.IsFailure.ShouldBeTrue();
        result.Error.Code.ShouldBe(Errors.Components.DoesNotExist.Code);

        componentRepositoryMock.Verify(repository => repository.DeleteAsync(It.IsAny<int>()), Times.Never);
    }

    [Fact]
    public async Task Delete_DeleteComponentAsync_WithExistingComponent_ReturnsSuccessResult()
    {
        // Arrange
        var id = 1;

        var mapperMock = new Mock<IMapper>();
        var componentRepositoryMock = new Mock<IComponentRepository>();
        componentRepositoryMock.Setup(repository => repository.ExistsAsync(id))
            .ReturnsAsync(true);

        var componentService = new ComponentService(componentRepositoryMock.Object, mapperMock.Object);

        // Act
        var result = await componentService.DeleteComponentAsync(id);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        componentRepositoryMock.Verify(repository => repository.DeleteAsync(id), Times.Once);
    }

    [Fact]
    public async Task Get_GetComponentVmForEditAsync_WithNonExistingComponent_ReturnsFailureResult()
    {
        // Arrange
        var id = 1;

        var mapperMock = new Mock<IMapper>();
        var componentRepositoryMock = new Mock<IComponentRepository>();
        componentRepositoryMock.Setup(repository => repository.GetByIdAsync(id))
            .ReturnsAsync((Component)null);

        var componentService = new ComponentService(componentRepositoryMock.Object, mapperMock.Object);

        // Act
        var result = await componentService.GetComponentVmForEditAsync(id);

        // Assert
        result.IsFailure.ShouldBeTrue();
        result.Error.Code.ShouldBe(Errors.Components.DoesNotExist.Code);

        componentRepositoryMock.Verify(repository => repository.GetByIdAsync(It.IsAny<int>()), Times.AtLeastOnce);
    }

    [Fact]
    public async Task Get_GetComponentVmForEditAsync_WithExistingComponent_ReturnsSuccessResult()
    {
        // Arrange
        var id = 1;
        var name = "ComponentName";

        var mapperMock = new Mock<IMapper>();
        var componentEntity = new Component { Id = id, Name = name };
        var componentVm = new ComponentVm { Id = id, Name = name };

        var componentRepositoryMock = new Mock<IComponentRepository>();
        componentRepositoryMock.Setup(repository => repository.GetByIdAsync(id))
            .ReturnsAsync(componentEntity);

        mapperMock.Setup(mapper => mapper.Map<ComponentVm>(componentEntity))
            .Returns(componentVm);

        var componentService = new ComponentService(componentRepositoryMock.Object, mapperMock.Object);

        // Act
        var result = await componentService.GetComponentVmForEditAsync(id);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldBe(componentVm);

        componentRepositoryMock.Verify(repository => repository.GetByIdAsync(id), Times.Once);
    }

    [Fact]
    public async Task Update_UpdateComponentAsync_WithNonExistingComponent_ReturnsFailureResult()
    {
        // Arrange
        var id = 1;
        var name = "ComponentName";

        var mapperMock = new Mock<IMapper>();
        var componentToEditVm = new ComponentVm { Id = id, Name = name };

        var componentRepositoryMock = new Mock<IComponentRepository>();
        componentRepositoryMock.Setup(repository => repository.ExistsAsync(id))
            .ReturnsAsync(false);

        var componentService = new ComponentService(componentRepositoryMock.Object, mapperMock.Object);

        // Act
        var result = await componentService.UpdateComponentAsync(componentToEditVm);

        // Assert
        result.IsFailure.ShouldBeTrue();
        result.Error.Code.ShouldBe(Errors.Components.DoesNotExist.Code);

        componentRepositoryMock.Verify(repository => repository.UpdateAsync(It.IsAny<Component>()), Times.Never);
    }

    [Fact]
    public async Task Update_UpdateComponentAsync_WithDuplicatedName_ReturnsFailureResult()
    {
        // Arrange
        var id = 1;
        var name = "ComponentName";

        var mapperMock = new Mock<IMapper>();
        var componentToEditVm = new ComponentVm { Id = id, Name = name };

        var componentRepositoryMock = new Mock<IComponentRepository>();
        componentRepositoryMock.Setup(repository => repository.ExistsAsync(id))
            .ReturnsAsync(true);
        componentRepositoryMock.Setup(repository => repository.DoesComponentWithNameExistAsync(componentToEditVm.Name))
            .ReturnsAsync(true);

        var componentService = new ComponentService(componentRepositoryMock.Object, mapperMock.Object);

        // Act
        var result = await componentService.UpdateComponentAsync(componentToEditVm);

        // Assert
        result.IsFailure.ShouldBeTrue();
        result.Error.Code.ShouldBe(Errors.Components.DuplicatedName.Code);

        componentRepositoryMock.Verify(repository => repository.UpdateAsync(It.IsAny<Component>()), Times.Never);
    }

    [Fact]
    public async Task Update_UpdateComponentAsync_WithUniqueName_ReturnsSuccessResult()
    {
        // Arrange
        var id = 1;
        var name = "ComponentName";

        var mapperMock = new Mock<IMapper>();
        var componentToEditVm = new ComponentVm { Id = id, Name = name };

        var componentRepositoryMock = new Mock<IComponentRepository>();
        componentRepositoryMock.Setup(repository => repository.ExistsAsync(id))
            .ReturnsAsync(true);
        componentRepositoryMock.Setup(repository => repository.DoesComponentWithNameExistAsync(componentToEditVm.Name))
            .ReturnsAsync(false);

        var componentService = new ComponentService(componentRepositoryMock.Object, mapperMock.Object);

        // Act
        var result = await componentService.UpdateComponentAsync(componentToEditVm);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        componentRepositoryMock.Verify(repository => repository.UpdateAsync(It.IsAny<Component>()), Times.Once);
    }
}