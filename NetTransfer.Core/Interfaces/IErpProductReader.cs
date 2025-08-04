using NetTransfer.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Core.Interfaces
{
    public interface IErpProductReader
    {
        Task<List<ProductDto>> GetProductsAsync();
    }
}
