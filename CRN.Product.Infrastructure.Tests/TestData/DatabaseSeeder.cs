using CRN.Product.Domain.Entities;
using CRN.Product.Infrastructure.Data;

namespace CRN.Product.Infrastructure.Tests.TestData;

public static class DatabaseSeeder
{
    public static void Seed(ApplicationDbContext context)
    {
        if (context.Product.Any())
            return;

        context.Product.AddRange(
            new ProductDetail
            {
                ProductName = "Laptop",
                CreatedBy = "Admin",
                CreatedOn = DateTime.UtcNow
            },
            new ProductDetail
            {
                ProductName = "Keyboard",
                CreatedBy = "Admin",
                CreatedOn = DateTime.UtcNow
            },
            new ProductDetail
            {
                ProductName = "Mouse",
                CreatedBy = "Admin",
                CreatedOn = DateTime.UtcNow
            });

        context.SaveChanges();
    }
}