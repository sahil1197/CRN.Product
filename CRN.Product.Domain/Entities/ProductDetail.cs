using System;
using System.Collections.Generic;
using System.Text;

namespace CRN.Product.Domain.Entities
{
    public class ProductDetail
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public ICollection<Item> Items { get; set; }
    }
}
