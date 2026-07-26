using CRN.Product.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRN.Product.Application.Interfaces
{
    public interface IProductRepository : IRepository<ProductDetail>
    {
        Task<ProductDetail?> GetProductWithItemsAsync(int id);
    }
}
