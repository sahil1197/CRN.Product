using CRN.Product.Application.DTOs.Authentication;
using CRN.Product.Application.Interfaces;
using CRN.Product.Domain.Entities;
using CRN.Product.Infrastructure.Data;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CRN.Product.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IJwtService _jwtService;
        private readonly PasswordHasher<User> _passwordHasher;

        public AuthService(
            ApplicationDbContext context,
            IJwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
            _passwordHasher = new PasswordHasher<User>();
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
        {
            var user = await _context.Users
                .Include(x => x.RefreshTokens)
                .FirstOrDefaultAsync(x => x.Username == request.Username);

            if (user == null)
                throw new UnauthorizedAccessException("Invalid username or password.");

            var result = _passwordHasher.VerifyHashedPassword(
                user,
                user.PasswordHash,
                request.Password);

            if (result == PasswordVerificationResult.Failed)
                throw new UnauthorizedAccessException("Invalid username or password.");

            var accessToken = _jwtService.GenerateAccessToken(user);

            var refreshToken = _jwtService.GenerateRefreshToken();

            user.RefreshTokens.Add(new RefreshToken
            {
                Token = refreshToken,
                Created = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddDays(7),
                IsRevoked = false
            });

            await _context.SaveChangesAsync();

            return new LoginResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                AccessTokenExpiry = DateTime.UtcNow.AddMinutes(30)
            };
        }

        public async Task<RefreshTokenResponseDto> RefreshTokenAsync(
            RefreshTokenRequestDto request)
        {
            var principal =
                _jwtService.GetPrincipalFromExpiredToken(request.AccessToken);

            var username = principal?.Identity?.Name;

            if (string.IsNullOrWhiteSpace(username))
                throw new SecurityTokenException("Invalid token.");

            var user = await _context.Users
                .Include(x => x.RefreshTokens)
                .FirstOrDefaultAsync(x => x.Username == username);

            if (user == null)
                throw new UnauthorizedAccessException();

            var storedToken = user.RefreshTokens
                .FirstOrDefault(x =>
                    x.Token == request.RefreshToken &&
                    !x.IsRevoked &&
                    x.Expires > DateTime.UtcNow);

            if (storedToken == null)
                throw new SecurityTokenException("Invalid refresh token.");

            storedToken.IsRevoked = true;

            var newAccessToken =
                _jwtService.GenerateAccessToken(user);

            var newRefreshToken =
                _jwtService.GenerateRefreshToken();

            user.RefreshTokens.Add(new RefreshToken
            {
                Token = newRefreshToken,
                Created = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddDays(7)
            });

            await _context.SaveChangesAsync();

            return new RefreshTokenResponseDto
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }
    }
}
