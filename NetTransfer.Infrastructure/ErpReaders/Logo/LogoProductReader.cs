using Microsoft.Data.SqlClient;
using NetTransfer.Core.Models;
using NetTransfer.Infrastructure.ErpReaders.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Infrastructure.ErpReaders.Logo
{
    public class LogoProductReader : ErpProductReaderBase
    {
        private readonly string _connectionString;

        public LogoProductReader(string connectionString)
        {
            _connectionString = connectionString;
        }

        public override async Task<List<ProductDto>> GetProductsAsync()
        {
            var products = new List<ProductDto>();
            using var conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();

            var cmd = new SqlCommand("SELECT CODE, NAME, PRICE, STOCK FROM PRODUCTS", conn);
            using var reader = await cmd.ExecuteReaderAsync();
            while (reader.Read())
            {
                products.Add(new ProductDto
                {
                    Code = reader["CODE"].ToString(),
                    Name = reader["NAME"].ToString(),
                    Price = SafeDecimal(reader["PRICE"]),
                    Stock = SafeInt(reader["STOCK"])
                });
            }

            return products;
        }
    }
}
