using NetTransfer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Core.Interfaces
{
    public interface ICommerceProductReader
    {
        Task<List<ProductDto>> GetProductsAsync();
    }
}
