using Microsoft.EntityFrameworkCore;
using NetTransfer.Data;
using NetTransfer.Integration;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransferService.Jobs
{
    [DisallowConcurrentExecution]
    public class CustomerSyncJob : IJob
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;
        private readonly NetTransferContext _context;

        public CustomerSyncJob(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _context = new NetTransferContext(_configuration.GetConnectionString("DefaultConnection")!.ToString());
        }
        public async Task Execute(IJobExecutionContext context)
        {
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<CustomerSyncJob>>();

                logger.LogInformation($"Cari aktarım görevi başladı : {DateTime.Now}");

                var transfer = await InitializeSettingAsync(logger);
                if (transfer == null)
                {
                    logger.LogError("Transfer ayarları alınamadı. Datetime : {time}", DateTime.Now);
                    return;
                }

                await transfer.CariTransfer();

                logger.LogInformation($"Cari aktarım görevi tamamlandı : {DateTime.Now}");
            }
        }

        public async Task<Transfer?> InitializeSettingAsync(ILogger<CustomerSyncJob> logger)
        {
            try
            {
                Transfer? transfer = null;
                if (!await _context.ErpSetting.AnyAsync())
                {
                    logger.LogError("Erp ayarlarını tanımlayın. Datetime : {time}", DateTime.Now);
                    return null;
                }

                var erpSetting = await _context.ErpSetting.FirstAsync();


                if (!await _context.VirtualStoreSetting.AnyAsync())
                {
                    logger.LogError("B2B ayarlarını tanımlayın. Datetime : {time}", DateTime.Now);
                    return null;
                }

                var virtualStoreSetting = await _context.VirtualStoreSetting.FirstAsync();
                var connectionString = _configuration.GetConnectionString("DefaultConnection")!.ToString();

                if (virtualStoreSetting.VirtualStore == "B2B")
                {
                    var parameter = await _context.B2BParameter.FirstAsync();
                    transfer = new Transfer(logger, connectionString, erpSetting, virtualStoreSetting, parameter);
                }
                else if (virtualStoreSetting.VirtualStore == "Smartstore")
                {
                    var parameter = await _context.SmartstoreParameter.FirstAsync();
                    transfer = new Transfer(logger, connectionString, erpSetting, virtualStoreSetting, parameter);
                }

                return transfer;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);

                return null;
            }
        }
    }
}
