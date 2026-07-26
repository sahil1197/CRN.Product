using CRN.Product.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace CRN.Product.Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateAccessToken(User user);

        string GenerateRefreshToken();

        ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
    }
}
