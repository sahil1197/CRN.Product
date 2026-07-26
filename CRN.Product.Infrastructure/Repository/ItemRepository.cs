using CRN.Product.Application.Interfaces;
using CRN.Product.Domain.Entities;
using CRN.Product.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRN.Product.Infrastructure.Repository
{
    public class ItemRepository
    : GenericRepository<Item>, IItemRepository
    {
        public ItemRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
