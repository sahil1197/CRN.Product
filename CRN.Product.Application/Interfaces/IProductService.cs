using CRN.Product.Application.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRN.Product.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync();

        Task<ProductResponseDto?> GetProductByIdAsync(int id);

        Task<ProductResponseDto> CreateProductAsync(CreateProductDto dto);

        Task<ProductResponseDto> UpdateProductAsync(int id, UpdateProductDto dto);

        Task DeleteProductAsync(int id);
    }
}
