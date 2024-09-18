using AutoMapper;
using Moq;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.Results;
using QueflityMVC.Application.Services;
using QueflityMVC.Application.ViewModels.Product;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;
using Shouldly;

namespace QueflityMVC.Application.UnitTests.Services;

public class ProductServiceTests
{
    private readonly Mock<IProductRepository> _productRepository;
    private readonly Mock<IMapper> _mapper;
    
    private readonly IProductService _productService;
    
    public ProductServiceTests()
    {
        _productRepository = new Mock<IProductRepository>();
        _mapper = new Mock<IMapper>();
        
        _productService = new ProductService(_mapper.Object, _productRepository.Object);
    }

    [Fact]
    public async Task Update_UpdateOrderAsync_InvalidOrder_MissingNumberInOrderNos_ReturnsError()
    {
        // Arrange
        var editOrderVm = new EditOrderVm
        {
            ProductsVMs = [
                new ProductVm { OrderNo = 0 },
                new ProductVm { OrderNo = 1 },
                new ProductVm { OrderNo = 3 }
            ]
        };

        // Act
        var result = await _productService.UpdateOrderAsync(editOrderVm);

        // Assert
        result.IsFailure.ShouldBeTrue();
        result.Error.ShouldBe(Errors.Product.InvalidOrder);
    }
    
    [Fact]
    public async Task Update_UpdateOrderAsync_InvalidOrder_NotAllProducts_ReturnsError()
    {
        // Arrange
        var editOrderVm = new EditOrderVm
        {
            ProductsVMs = [
                new ProductVm { OrderNo = 0 },
                new ProductVm { OrderNo = 1 },
                new ProductVm { OrderNo = 2 }
            ]
        };
        
        _productRepository.Setup(x => x.AreTheseAllVisibleProductsAsync(It.IsAny<List<Product>>()))
            .ReturnsAsync(false);

        // Act
        var result = await _productService.UpdateOrderAsync(editOrderVm);

        // Assert
        result.IsFailure.ShouldBeTrue();
        result.Error.ShouldBe(Errors.Product.ProductMissingInOrder);
    }
    
    [Fact]
    public async Task Update_UpdateOrderAsync_ReturnsSuccess()
    {
        // Arrange
        var editOrderVm = new EditOrderVm
        {
            ProductsVMs = [
                new ProductVm { OrderNo = 0 },
                new ProductVm { OrderNo = 1 },
                new ProductVm { OrderNo = 2 }
            ]
        };
        
        _productRepository.Setup(x => x.AreTheseAllVisibleProductsAsync(It.IsAny<List<Product>>()))
            .ReturnsAsync(true);

        // Act
        var result = await _productService.UpdateOrderAsync(editOrderVm);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        
        _productRepository.Verify(x=>x.AreTheseAllVisibleProductsAsync(It.IsAny<List<Product>>()), Times.AtLeastOnce);
        _productRepository.Verify(x => x.UpdateProductsOrderAsync(It.IsAny<List<Product>>()), Times.Once);
    }
}