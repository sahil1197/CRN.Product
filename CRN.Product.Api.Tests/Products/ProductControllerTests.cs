using System.Net;
using CRN.Product.Api.Tests.Helpers;
using FluentAssertions;
using Xunit;

namespace CRN.Product.Api.Tests.Products;

public class ProductsControllerTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public ProductsControllerTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetAllProducts_ShouldReturnSuccess()
    {
        // Arrange

        // Act
        var response = await _client.GetAsync("/api/v1/products");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsStringAsync();

        content.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task GetProductByInvalidId_ShouldReturnNotFound()
    {
        // Arrange
        const int invalidId = 99999;

        // Act
        var response = await _client.GetAsync($"/api/v1/products/{invalidId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}