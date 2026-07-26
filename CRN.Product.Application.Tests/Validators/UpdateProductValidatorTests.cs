using CRN.Product.Application.DTOs.Product;
using CRN.Product.Application.Validators;
using FluentValidation.TestHelper;
using Xunit;

namespace CRN.Product.Application.Tests.Validators
{
    public class UpdateProductValidatorTests
    {
        private readonly UpdateProductValidator _validator;

        public UpdateProductValidatorTests()
        {
            _validator = new UpdateProductValidator();
        }

        [Fact]
        public void Should_Not_Have_Error_When_Request_Is_Valid()
        {
            var model = new UpdateProductDto
            {
                Id = 1,
                ProductName = "Gaming Laptop"
            };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(x => x.Id);
            result.ShouldNotHaveValidationErrorFor(x => x.ProductName);
        }

        [Fact]
        public void Should_Have_Error_When_Id_Is_Zero()
        {
            var model = new UpdateProductDto
            {
                Id = 0,
                ProductName = "Laptop"
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Id);
        }

        [Fact]
        public void Should_Have_Error_When_ProductName_Is_Empty()
        {
            var model = new UpdateProductDto
            {
                Id = 1,
                ProductName = string.Empty
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.ProductName);
        }

        [Fact]
        public void Should_Have_Error_When_ProductName_Is_Null()
        {
            var model = new UpdateProductDto
            {
                Id = 1,
                ProductName = null
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.ProductName);
        }

        [Fact]
        public void Should_Have_Error_When_ProductName_Exceeds_Max_Length()
        {
            var model = new UpdateProductDto
            {
                Id = 1,
                ProductName = new string('A', 101)
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.ProductName);
        }
    }
}