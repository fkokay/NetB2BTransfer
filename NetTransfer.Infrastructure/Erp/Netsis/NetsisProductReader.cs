using NetTransfer.Core.Dtos;
using NetTransfer.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Infrastructure.Erp.Netsis
{
    public class NetsisProductReader : IErpProductReader
    {
        private readonly string _connectionString;

        public NetsisProductReader(string connectionString)
        {
            _connectionString = connectionString;
        }
        public Task<List<ProductDto>> GetProductsAsync()
        {
            var products = new List<ProductDto>();

            throw new NotImplementedException();
        }
    }
}
