using System;
using System.Collections.Generic;
using System.Text;

namespace CRN.Product.Domain.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }

        public string Token { get; set; } = string.Empty;

        public DateTime Expires { get; set; }

        public DateTime Created { get; set; }

        public bool IsRevoked { get; set; }

        public int UserId { get; set; }

        public User User { get; set; } = null!;
    }
}
