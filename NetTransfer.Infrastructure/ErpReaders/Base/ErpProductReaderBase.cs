using NetTransfer.Core.Interfaces;
using NetTransfer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Infrastructure.ErpReaders.Base
{
    public abstract class ErpProductReaderBase : IErpProductReader
    {
        public abstract List<ProductDto> GetProducts();

        protected decimal SafeDecimal(object val)
        {
            return decimal.TryParse(val?.ToString(), out var result) ? result : 0;
        }

        protected int SafeInt(object val)
        {
            return int.TryParse(val?.ToString(), out var result) ? result : 0;
        }
    }
}
