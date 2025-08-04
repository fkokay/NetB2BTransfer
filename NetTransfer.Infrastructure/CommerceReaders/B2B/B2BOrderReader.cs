using NetTransfer.Core.Dtos;
using NetTransfer.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Infrastructure.CommerceReaders.B2B
{
    public class B2BOrderReader : ICommerceOrderReader
    {
        public Task<List<OrderDto>> GetNewOrdersAsync()
        {
            throw new NotImplementedException();
        }
    }
}
