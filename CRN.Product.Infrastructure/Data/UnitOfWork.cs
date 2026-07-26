using CRN.Product.Application.Interfaces;
using CRN.Product.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRN.Product.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IProductRepository Products { get; }

        public IItemRepository Items { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            Products = new ProductRepository(context);

            Items = new ItemRepository(context);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
