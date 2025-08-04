using Microsoft.Data.SqlClient;
using NetTransfer.Core.Dtos;
using NetTransfer.Core.Interfaces;
using NetTransfer.Infrastructure.ErpReaders.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Infrastructure.ErpReaders.Logo
{
    public class LogoProductReader : IErpProductReader
    {
        private readonly string _connectionString;

        public LogoProductReader(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<ProductDto>> GetProductsAsync()
        {
            var result = new List<ProductDto>();

            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            string query = @"SELECT CODE, NAME, BARCODE, PRICE, STOCK FROM LG_001_ITEMS";

            using var command = new SqlCommand(query, connection);
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                result.Add(new ProductDto
                {
                    Code = reader["CODE"] + "",
                    Name = reader["NAME"] + "",
                    Barcode = reader["BARCODE"] + "",
                    Price = Convert.ToDecimal(reader["PRICE"]),
                    Stock = Convert.ToInt32(reader["STOCK"])
                });
            }

            return result;
        }
    }
}
