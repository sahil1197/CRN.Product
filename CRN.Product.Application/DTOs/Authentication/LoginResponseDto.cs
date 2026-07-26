using System;
using System.Collections.Generic;
using System.Text;

namespace CRN.Product.Application.DTOs.Authentication
{
    public class LoginResponseDto
    {
        public string AccessToken { get; set; } = string.Empty;

        public string RefreshToken { get; set; } = string.Empty;

        public DateTime AccessTokenExpiry { get; set; }
    }
}
