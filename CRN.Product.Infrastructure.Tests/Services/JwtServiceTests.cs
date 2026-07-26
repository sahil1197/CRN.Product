using CRN.Product.Application.Authentication;
using CRN.Product.Domain.Entities;
using CRN.Product.Infrastructure.Identity;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Xunit;

namespace CRN.Product.Infrastructure.Tests.Services;

public class JwtServiceTests
{
    private readonly JwtService _jwtService;

    public JwtServiceTests()
    {
        var settings = Options.Create(new JwtSettings
        {
            SecretKey = "ThisIsAVeryStrongSecretKeyForJwt123456789",
            Issuer = "CRN",
            Audience = "CRNUsers",
            AccessTokenExpirationMinutes = 30
        });

        _jwtService = new JwtService(settings);
    }

    [Fact]
    public void GenerateAccessToken_Should_Return_Token()
    {
        var user = new User
        {
            Id = 1,
            Username = "admin",
            Role = "Admin"
        };

        var token = _jwtService.GenerateAccessToken(user);

        token.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public void GenerateRefreshToken_Should_Return_Token()
    {
        var token = _jwtService.GenerateRefreshToken();

        token.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public void GenerateRefreshToken_Should_Be_Unique()
    {
        var token1 = _jwtService.GenerateRefreshToken();

        var token2 = _jwtService.GenerateRefreshToken();

        token1.Should().NotBe(token2);
    }
}