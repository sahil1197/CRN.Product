using CRN.Product.Domain.Entities;
using CRN.Product.Infrastructure.Data;
using CRN.Product.Infrastructure.Repository;
using CRN.Product.Infrastructure.Tests.Data;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Internal;
using Xunit;

namespace CRN.Product.Infrastructure.Tests.Repositories;

public class ProductRepositoryTests
{
    [Fact]
    public async Task GetProductWithItemsAsync_Should_Return_Product()
    {
        using var context = DbContextFactory.CreateDbContext();

        var product = new ProductDetail
        {
            ProductName = "Laptop",
            CreatedBy = "Admin",
            CreatedOn = DateTime.UtcNow,
           
        };

        context.Product.Add(product);

        await context.SaveChangesAsync();

        var repository = new ProductRepository(context);

        var result =
            await repository.GetProductWithItemsAsync(product.Id);

        result.Should().NotBeNull();

        result!.Items.Should().HaveCount(1);
    }
}