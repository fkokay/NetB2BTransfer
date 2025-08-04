using NetTransfer.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Core.Interfaces
{
    public interface ICommerceProductWriter
    {
        Task<bool> SendProductsAsync(List<ProductDto> products);
    }
}
