using System.Net;
using System.Net.Http.Json;
using CRN.Product.Api.Tests.Helpers;
using FluentAssertions;
using Xunit;

namespace CRN.Product.Api.Tests.Authentication;

public class AuthControllerTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public AuthControllerTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Login_WithValidCredentials_ShouldReturnOk()
    {
        // Arrange
        var loginRequest = new
        {
            Username = "admin",
            Password = "Admin@123"
        };

        // Act
        var response = await _client.PostAsJsonAsync(
            "/api/v1/auth/login",
            loginRequest);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsStringAsync();

        content.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Login_WithInvalidCredentials_ShouldReturnUnauthorized()
    {
        // Arrange
        var loginRequest = new
        {
            Username = "admin",
            Password = "WrongPassword"
        };

        // Act
        var response = await _client.PostAsJsonAsync(
            "/api/v1/auth/login",
            loginRequest);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}