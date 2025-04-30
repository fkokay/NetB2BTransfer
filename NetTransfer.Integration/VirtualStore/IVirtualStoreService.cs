using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Integration.VirtualStore
{
    public interface IVirtualStoreService<T> where T : class
    {
        public List<T> MappingProduct(string erp,object? data,ref string errorMessage);
    }
}
