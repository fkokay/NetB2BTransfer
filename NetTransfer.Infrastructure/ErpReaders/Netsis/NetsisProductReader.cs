using NetTransfer.Core.Dtos;
using NetTransfer.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Infrastructure.ErpReaders.Netsis
{
    public class NetsisProductReader : IErpProductReader
    {
        public Task<List<ProductDto>> GetProductsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
