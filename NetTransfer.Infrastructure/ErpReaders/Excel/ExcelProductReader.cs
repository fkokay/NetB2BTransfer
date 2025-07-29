using NetTransfer.Core.Interfaces;
using NetTransfer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Infrastructure.ErpReaders.Excel
{
    public class ExcelProductReader : IErpProductReader
    {
        private readonly string _filePath;

        public ExcelProductReader(string filePath)
        {
            _filePath = filePath;
        }

        public Task<List<ProductDto>> GetProductsAsync()
        {
            var list = new List<ProductDto>();

            using var package = new OfficeOpenXml.ExcelPackage(new FileInfo(_filePath));
            var worksheet = package.Workbook.Worksheets.First();
            int rowCount = worksheet.Dimension.Rows;

            for (int row = 2; row <= rowCount; row++)
            {
                list.Add(new ProductDto
                {
                    Code = worksheet.Cells[row, 1].Text,
                    Name = worksheet.Cells[row, 2].Text,
                    Price = decimal.Parse(worksheet.Cells[row, 3].Text),
                    Stock = int.Parse(worksheet.Cells[row, 4].Text),
                });
            }

            return Task.FromResult(list);
        }
    }
}
