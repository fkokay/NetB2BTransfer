using NetTransfer.Core.Interfaces;
using NetTransfer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Infrastructure.CommerceReaders.Smartstore
{
    internal class SmartStoreOrderReader : ICommerceOrderReader
    {
        public Task<List<OrderDto>> GetNewOrdersAsync()
        {
            throw new NotImplementedException();
        }
    }
}
