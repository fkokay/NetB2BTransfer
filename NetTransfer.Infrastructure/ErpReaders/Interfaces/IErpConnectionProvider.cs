using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Infrastructure.ErpReaders.Interfaces
{
    public interface IErpConnectionProvider
    {
        string GetConnectionString();
    }
}
