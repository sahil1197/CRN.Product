using AutoMapper;
using CRN.Product.Application.DTOs.Product;
using CRN.Product.Application.Interfaces;
using CRN.Product.Application.Services;
using CRN.Product.Application.Tests.TestData;
using CRN.Product.Domain.Entities;
using CRN.Product.Domain.Exceptions;
using FluentAssertions;
using Moq;
using Xunit;

namespace CRN.Product.Application.Tests.Services;

public class ProductServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;

    private readonly ProductService _service;

    public ProductServiceTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _productRepositoryMock = new Mock<IProductRepository>();
        _mapperMock = new Mock<IMapper>();

        _unitOfWorkMock
            .Setup(x => x.Products)
            .Returns(_productRepositoryMock.Object);

        _service = new ProductService(
            _unitOfWorkMock.Object,
            _mapperMock.Object);
    }

    [Fact]
    public async Task GetAllProductsAsync_ShouldReturnProducts()
    {
        // Arrange
        var products = ProductTestData.Products;

        var response = new List<ProductResponseDto>
        {
            new ProductResponseDto
            {
                Id = 1,
                ProductName = "Laptop"
            },
            new ProductResponseDto
            {
                Id = 2,
                ProductName = "Keyboard"
            }
        };

        _productRepositoryMock
            .Setup(x => x.GetAllAsync())
            .ReturnsAsync(products);

        _mapperMock
            .Setup(x => x.Map<IEnumerable<ProductResponseDto>>(products))
            .Returns(response);

        // Act
        var result = await _service.GetAllProductsAsync();

        // Assert
        result.Should().HaveCount(2);
    }

    [Fact]
    public async Task GetProductByIdAsync_ShouldReturnProduct()
    {
        // Arrange
        var product = ProductTestData.Product;

        var dto = new ProductResponseDto
        {
            Id = product.Id,
            ProductName = product.ProductName
        };

        _productRepositoryMock
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(product);

        _mapperMock
            .Setup(x => x.Map<ProductResponseDto>(product))
            .Returns(dto);

        // Act
        var result = await _service.GetProductByIdAsync(1);

        // Assert
        result.Should().NotBeNull();
        result!.ProductName.Should().Be("Laptop");
    }

    [Fact]
    public async Task GetProductByIdAsync_ShouldThrow_WhenProductNotFound()
    {
        _productRepositoryMock
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync((ProductDetail?)null);

        await Assert.ThrowsAsync<NotFoundException>(() =>
            _service.GetProductByIdAsync(1));
    }

    [Fact]
    public async Task CreateProductAsync_ShouldCreateProduct()
    {
        var dto = ProductTestData.CreateProduct;

        var entity = new ProductDetail
        {
            ProductName = dto.ProductName
        };

        var response = new ProductResponseDto
        {
            ProductName = dto.ProductName
        };

        _mapperMock
            .Setup(x => x.Map<ProductDetail>(dto))
            .Returns(entity);

        _mapperMock
            .Setup(x => x.Map<ProductResponseDto>(entity))
            .Returns(response);

        var result = await _service.CreateProductAsync(dto);

        _productRepositoryMock.Verify(
            x => x.AddAsync(It.IsAny<ProductDetail>()),
            Times.Once);

        _unitOfWorkMock.Verify(
            x => x.SaveChangesAsync(),
            Times.Once);

        result.Should().NotBeNull();
    }

    [Fact]
    public async Task DeleteProductAsync_ShouldDeleteProduct()
    {
        var product = ProductTestData.Product;

        _productRepositoryMock
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(product);

        await _service.DeleteProductAsync(1);

        _productRepositoryMock.Verify(
            x => x.Delete(product),
            Times.Once);

        _unitOfWorkMock.Verify(
            x => x.SaveChangesAsync(),
            Times.Once);
    }
}