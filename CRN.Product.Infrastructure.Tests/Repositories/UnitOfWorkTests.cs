using CRN.Product.Domain.Entities;
using CRN.Product.Infrastructure.Data;
using CRN.Product.Infrastructure.Tests.Data;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Internal;
using Xunit;

namespace CRN.Product.Infrastructure.Tests.Repositories;

public class UnitOfWorkTests
{
    [Fact]
    public async Task SaveChangesAsync_Should_Save_Data()
    {
        using var context = DbContextFactory.CreateDbContext();

        var unitOfWork = new UnitOfWork(context);

        context.Product.Add(new ProductDetail
        {
            ProductName = "Laptop",
            CreatedBy = "Admin",
            CreatedOn = DateTime.UtcNow
        });

        await unitOfWork.SaveChangesAsync();

        context.Product.Count()
            .Should()
            .Be(1);
    }

    [Fact]
    public void Products_Should_Not_Be_Null()
    {
        using var context = DbContextFactory.CreateDbContext();

        var unitOfWork = new UnitOfWork(context);

        unitOfWork.Products.Should().NotBeNull();
    }

    [Fact]
    public void Items_Should_Not_Be_Null()
    {
        using var context = DbContextFactory.CreateDbContext();

        var unitOfWork = new UnitOfWork(context);

        unitOfWork.Items.Should().NotBeNull();
    }
}