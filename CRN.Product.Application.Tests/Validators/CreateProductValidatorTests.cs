using CRN.Product.Application.DTOs.Product;
using CRN.Product.Application.Validators;
using FluentValidation.TestHelper;
using Xunit;

namespace CRN.Product.Application.Tests.Validators
{
    public class CreateProductValidatorTests
    {
        private readonly CreateProductValidator _validator;

        public CreateProductValidatorTests()
        {
            _validator = new CreateProductValidator();
        }

        [Fact]
        public void Should_Not_Have_Error_When_ProductName_Is_Valid()
        {
            // Arrange
            var model = new CreateProductDto
            {
                ProductName = "Laptop"
            };

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.ProductName);
        }

        [Fact]
        public void Should_Have_Error_When_ProductName_Is_Empty()
        {
            // Arrange
            var model = new CreateProductDto
            {
                ProductName = string.Empty
            };

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.ProductName);
        }

        [Fact]
        public void Should_Have_Error_When_ProductName_Is_Null()
        {
            // Arrange
            var model = new CreateProductDto
            {
                ProductName = null
            };

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.ProductName);
        }

        [Fact]
        public void Should_Have_Error_When_ProductName_Exceeds_Max_Length()
        {
            // Arrange
            var model = new CreateProductDto
            {
                ProductName = new string('A', 101)
            };

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.ProductName);
        }

        [Fact]
        public void Should_Not_Have_Error_When_ProductName_Is_Max_Length()
        {
            // Arrange
            var model = new CreateProductDto
            {
                ProductName = new string('A', 100)
            };

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.ProductName);
        }
    }
}