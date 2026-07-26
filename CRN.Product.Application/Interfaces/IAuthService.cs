using CRN.Product.Application.DTOs.Authentication;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRN.Product.Application.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(LoginRequestDto request);

        Task<RefreshTokenResponseDto> RefreshTokenAsync(
            RefreshTokenRequestDto request);
    }
}
