using System;
using System.Collections.Generic;
using System.Text;

namespace CRN.Product.Application.DTOs.Common
{
    public class ErrorResponseDto
    {
        public int StatusCode { get; set; }

        public string Message { get; set; } = string.Empty;

        public string? Details { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
