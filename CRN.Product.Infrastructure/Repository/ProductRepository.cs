using CRN.Product.Application.Interfaces;
using CRN.Product.Domain.Entities;
using CRN.Product.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRN.Product.Infrastructure.Repository
{
    public class ProductRepository
    : GenericRepository<ProductDetail>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<ProductDetail?> GetProductWithItemsAsync(int id)
        {
            return await _context.Product
                .Include(p => p.Items)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
