using System;
using System.Collections.Generic;
using System.Text;

namespace CRN.Product.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IProductRepository Products { get; }

        IItemRepository Items { get; }

        Task<int> SaveChangesAsync();
    }
}
