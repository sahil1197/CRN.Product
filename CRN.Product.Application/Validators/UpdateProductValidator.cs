using FluentValidation;
using CRN.Product.Application.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRN.Product.Application.Validators
{
    public class UpdateProductValidator : AbstractValidator<UpdateProductDto>
    {
        public UpdateProductValidator()
        {
            RuleFor(x => x.ProductName)
                .NotEmpty()
                .WithMessage("Product Name is required.")
                .MaximumLength(255)
                .WithMessage("Product Name cannot exceed 255 characters.");
        }
    }
}
