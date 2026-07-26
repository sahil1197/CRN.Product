using FluentValidation;
using CRN.Product.Application.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRN.Product.Application.Validators
{
    public class CreateProductValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.ProductName)
                .NotEmpty()
                .WithMessage("Product Name is required.")
                .MaximumLength(255)
                .WithMessage("Product Name cannot exceed 255 characters.");
        }
    }
}
