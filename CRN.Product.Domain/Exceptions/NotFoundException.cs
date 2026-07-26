using System;
using System.Collections.Generic;
using System.Text;

namespace CRN.Product.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message)
            : base(message)
        {
        }
    }
}
