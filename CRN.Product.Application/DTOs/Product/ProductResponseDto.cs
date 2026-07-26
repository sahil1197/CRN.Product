using System;
using System.Collections.Generic;
using System.Text;

namespace CRN.Product.Application.DTOs.Product
{
    public class ProductResponseDto
    {
        public int Id { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public string CreatedBy { get; set; } = string.Empty;

        public DateTime CreatedOn { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
