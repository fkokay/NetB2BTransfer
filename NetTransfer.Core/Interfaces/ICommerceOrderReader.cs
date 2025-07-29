using NetTransfer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Core.Interfaces
{
    public interface ICommerceOrderReader
    {
        Task<List<OrderDto>> GetNewOrdersAsync();
    }
}
