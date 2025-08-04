using NetTransfer.Core.Dtos;
using NetTransfer.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Infrastructure.CommerceWriters.B2B
{
    public class B2BProductWriter : ICommerceProductWriter
    {
        public Task<bool> SendProductsAsync(List<ProductDto> products)
        {
            throw new NotImplementedException();
        }
    }
}
