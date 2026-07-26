using CRN.Product.Domain.Entities;
using CRN.Product.Infrastructure.Data;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CRN.Product.Infrastructure.Tests.Data
{
    public class ApplicationDbContextTests
    {
        private ApplicationDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task Add_Product_Should_Save_To_Database()
        {
            // Arrange
            using var context = CreateDbContext();

            var product = new ProductDetail
            {
                ProductName = "Laptop",
                CreatedBy = "Admin",
                CreatedOn = DateTime.UtcNow
            };

            // Act
            await context.Product.AddAsync(product);
            await context.SaveChangesAsync();

            // Assert
            context.Product.Count().Should().Be(1);
        }

        [Fact]
        public async Task Get_Product_By_Id_Should_Return_Product()
        {
            // Arrange
            using var context = CreateDbContext();

            var product = new ProductDetail
            {
                ProductName = "Keyboard",
                CreatedBy = "Admin",
                CreatedOn = DateTime.UtcNow
            };

            await context.Product.AddAsync(product);
            await context.SaveChangesAsync();

            // Act
            var result = await context.Product
                                      .FirstOrDefaultAsync(x => x.Id == product.Id);

            // Assert
            result.Should().NotBeNull();
            result!.ProductName.Should().Be("Keyboard");
        }

        [Fact]
        public async Task Update_Product_Should_Update_Database()
        {
            // Arrange
            using var context = CreateDbContext();

            var product = new ProductDetail
            {
                ProductName = "Mouse",
                CreatedBy = "Admin",
                CreatedOn = DateTime.UtcNow
            };

            await context.Product.AddAsync(product);
            await context.SaveChangesAsync();

            // Act
            product.ProductName = "Gaming Mouse";

            context.Product.Update(product);
            await context.SaveChangesAsync();

            // Assert
            var updated = await context.Product.FindAsync(product.Id);

            updated.Should().NotBeNull();
            updated!.ProductName.Should().Be("Gaming Mouse");
        }

        [Fact]
        public async Task Delete_Product_Should_Remove_From_Database()
        {
            // Arrange
            using var context = CreateDbContext();

            var product = new ProductDetail
            {
                ProductName = "Monitor",
                CreatedBy = "Admin",
                CreatedOn = DateTime.UtcNow
            };

            await context.Product.AddAsync(product);
            await context.SaveChangesAsync();

            // Act
            context.Product.Remove(product);
            await context.SaveChangesAsync();

            // Assert
            context.Product.Should().BeEmpty();
        }

        [Fact]
        public async Task Get_All_Product_Should_Return_All_Records()
        {
            // Arrange
            using var context = CreateDbContext();

            await context.Product.AddRangeAsync(
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

            await context.SaveChangesAsync();

            // Act
            var Product = await context.Product.ToListAsync();

            // Assert
            Product.Should().HaveCount(3);
        }
    }
}