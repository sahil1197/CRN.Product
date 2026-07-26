using AutoMapper;
using CRN.Product.Application.DTOs.Product;
using CRN.Product.Application.Mapping;
using CRN.Product.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace CRN.Product.Application.Tests.Mapping
{
    public class MappingProfileTests
    {
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configuration;

        public MappingProfileTests()
        {
            _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductMappingProfile>();
            });

            _mapper = _configuration.CreateMapper();
        }

        [Fact]
        public void AutoMapper_Configuration_Should_Be_Valid()
        {
            // Act & Assert
            _configuration.AssertConfigurationIsValid();
        }

        [Fact]
        public void Should_Map_ProductDetail_To_ProductResponseDto()
        {
            // Arrange
            var product = new ProductDetail
            {
                Id = 1,
                ProductName = "Laptop",
                CreatedBy = "Admin",
                CreatedOn = DateTime.UtcNow
            };

            // Act
            var result = _mapper.Map<ProductResponseDto>(product);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(product.Id);
            result.ProductName.Should().Be(product.ProductName);
        }

        [Fact]
        public void Should_Map_CreateProductDto_To_ProductDetail()
        {
            // Arrange
            var dto = new CreateProductDto
            {
                ProductName = "Keyboard"
            };

            // Act
            var result = _mapper.Map<ProductDetail>(dto);

            // Assert
            result.Should().NotBeNull();
            result.ProductName.Should().Be(dto.ProductName);
        }

        [Fact]
        public void Should_Map_UpdateProductDto_To_ProductDetail()
        {
            // Arrange
            var dto = new UpdateProductDto
            {
                Id = 1,
                ProductName = "Gaming Keyboard"
            };

            // Act
            var result = _mapper.Map<ProductDetail>(dto);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(dto.Id);
            result.ProductName.Should().Be(dto.ProductName);
        }
    }
}