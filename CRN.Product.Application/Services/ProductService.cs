using AutoMapper;
using CRN.Product.Application.DTOs.Product;
using CRN.Product.Application.Interfaces;
using CRN.Product.Domain.Entities;
using CRN.Product.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRN.Product.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all products
        /// </summary>
        public async Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync()
        {
            var products = await _unitOfWork.Products.GetAllAsync();

            return _mapper.Map<IEnumerable<ProductResponseDto>>(products);
        }

        /// <summary>
        /// Get product by Id
        /// </summary>
        public async Task<ProductResponseDto?> GetProductByIdAsync(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);

            if (product == null)
                throw new NotFoundException($"Product with Id {id} was not found.");

            return _mapper.Map<ProductResponseDto>(product);
        }

        /// <summary>
        /// Create new product
        /// </summary>
        public async Task<ProductResponseDto> CreateProductAsync(CreateProductDto dto)
        {
            var product = _mapper.Map<ProductDetail>(dto);

            product.CreatedBy = "Admin";
            product.CreatedOn = DateTime.UtcNow;

            await _unitOfWork.Products.AddAsync(product);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ProductResponseDto>(product);
        }

        /// <summary>
        /// Update existing product
        /// </summary>
        public async Task<ProductResponseDto> UpdateProductAsync(int id, UpdateProductDto dto)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);

            if (product == null)
                throw new NotFoundException($"Product with Id {id} was not found.");

            // Update fields
            product.ProductName = dto.ProductName;
            product.ModifiedBy = "Admin";
            product.ModifiedOn = DateTime.UtcNow;

            _unitOfWork.Products.Update(product);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ProductResponseDto>(product);
        }

        /// <summary>
        /// Delete product
        /// </summary>
        public async Task DeleteProductAsync(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);

            if (product == null)
                throw new NotFoundException($"Product with Id {id} was not found.");

            _unitOfWork.Products.Delete(product);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
