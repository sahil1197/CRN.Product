using System;
using System.Collections.Generic;
using System.Text;

namespace CRN.Product.Domain.Entities
{
    public class Item
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public ProductDetail Product { get; set; }
    }
}
