using System;
using System.Collections.Generic;
using System.Text;

namespace CRN.Product.Application.DTOs.Product
{
    public class UpdateProductDto
    {
        public int? Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
    }
}
