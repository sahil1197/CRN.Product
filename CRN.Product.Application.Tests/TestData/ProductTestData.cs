using CRN.Product.Application.DTOs.Product;
using CRN.Product.Domain.Entities;

namespace CRN.Product.Application.Tests.TestData;

public static class ProductTestData
{
    public static ProductDetail Product => new()
    {
        Id = 1,
        ProductName = "Laptop",
        CreatedBy = "Admin",
        CreatedOn = DateTime.UtcNow,
        ModifiedBy = null,
        ModifiedOn = null,
        Items = new List<Item>
        {
            new Item
            {
                Id = 1,
                ProductId = 1,
                Quantity = 10
            },
            new Item
            {
                Id = 2,
                ProductId = 1,
                Quantity = 20
            }
        }
    };

    public static List<ProductDetail> Products => new()
    {
        new ProductDetail
        {
            Id = 1,
            ProductName = "Laptop",
            CreatedBy = "Admin",
            CreatedOn = DateTime.UtcNow,
            Items = new List<Item>
            {
                new Item
                {
                    Id = 1,
                    ProductId = 1,
                    Quantity = 10
                }
            }
        },

        new ProductDetail
        {
            Id = 2,
            ProductName = "Keyboard",
            CreatedBy = "Admin",
            CreatedOn = DateTime.UtcNow,
            Items = new List<Item>
            {
                new Item
                {
                    Id = 2,
                    ProductId = 2,
                    Quantity = 15
                }
            }
        },

        new ProductDetail
        {
            Id = 3,
            ProductName = "Mouse",
            CreatedBy = "Admin",
            CreatedOn = DateTime.UtcNow,
            Items = new List<Item>
            {
                new Item
                {
                    Id = 3,
                    ProductId = 3,
                    Quantity = 30
                }
            }
        }
    };

    public static CreateProductDto CreateProduct => new()
    {
        ProductName = "Monitor"
    };

    public static UpdateProductDto UpdateProduct => new()
    {
        Id = 1,
        ProductName = "Gaming Laptop"
    };
}