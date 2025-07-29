using Microsoft.Extensions.DependencyInjection;
using NetTransfer.Core.Interfaces;
using NetTransfer.Infrastructure.CommerceWriters.B2B;
using NetTransfer.Infrastructure.CommerceWriters.SmartStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Infrastructure.CommerceWriters.Common
{
    public class WriterFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public WriterFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ICommerceProductWriter GetWriter(string platform)
        {
            return platform switch
            {
                "SmartStore" => _serviceProvider.GetRequiredService<SmartStoreProductWriter>(),
                "B2B" => _serviceProvider.GetRequiredService<B2BProductWriter>(),
                _ => throw new NotImplementedException($"Platform '{platform}' not supported.")
            };
        }
    }
}
