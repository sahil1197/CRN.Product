using System;
using System.Collections.Generic;
using System.Text;

namespace CRN.Product.Application.DTOs.Common
{
    public class PaginationRequestDto
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }
}
