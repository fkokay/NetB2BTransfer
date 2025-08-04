using NetTransfer.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Core.Helpers
{
    public static class ValidationHelper
    {
        public static bool IsValidProduct(ProductDto p)
        {
            return !string.IsNullOrEmpty(p.Code) && p.Price > 0;
        }
    }
}
