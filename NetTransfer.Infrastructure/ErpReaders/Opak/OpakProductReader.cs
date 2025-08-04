using NetTransfer.Core.Dtos;
using NetTransfer.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Infrastructure.ErpReaders.Opak
{
    public class OpakProductReader : IErpProductReader
    {
        public OpakProductReader()
        {
        }

        public Task<List<ProductDto>> GetProductsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
