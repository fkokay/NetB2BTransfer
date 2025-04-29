using Microsoft.Extensions.Logging;
using NetTransfer.Core.Entities;
using NetTransfer.Core.TaskSchedulers;
using NetTransfer.Data;
using NetTransfer.Integration;

namespace NetTransferService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly NetTransferContext _context;
        private readonly IConfiguration _configuration;

        private NetTaskScheduler taskScheduler = new NetTaskScheduler();
        private ErpSetting? _erpSetting;
        private VirtualStoreSetting? _b2bSetting;

        private B2BParameter? _b2BParameter;
        private SmartstoreParameter? _smartstoreParameter;
        private Transfer transfer;

        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _context = new NetTransferContext(_configuration.GetConnectionString("DefaultConnection")!.ToString()); ;

            InitializeSetting();
        }

        public void InitializeSetting()
        {
            _erpSetting = _context.ErpSetting.FirstOrDefault();
            if (_erpSetting == null)
            {
                _logger.LogInformation("Erp ayarlarýný tanýmlayýn. Datetime : {time}", DateTimeOffset.Now);
                return;
            }

            _b2bSetting = _context.VirtualStoreSetting.FirstOrDefault();
            if (_b2bSetting == null)
            {
                _logger.LogInformation("B2B ayarlarýný tanýmlayýn. Datetime : {time}", DateTimeOffset.Now);
                return;
            }

            if (_b2bSetting.VirtualStore == "B2B")
            {
                _b2bSetting = _context.VirtualStoreSetting.FirstOrDefault();
                transfer = new Transfer(_logger, _erpSetting, _b2bSetting, _b2BParameter);
            }
            else if (_b2bSetting.VirtualStore == "Smartstore")
            {
                _smartstoreParameter = _context.SmartstoreParameter.FirstOrDefault();
                transfer = new Transfer(_logger, _erpSetting, _b2bSetting, _smartstoreParameter);
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Factory.StartNew(() => MalzemeTransfer(), CancellationToken.None, TaskCreationOptions.None, taskScheduler);
            await Task.Factory.StartNew(() => MalzemeStokTransfer(), CancellationToken.None, TaskCreationOptions.None, taskScheduler);
            await Task.Factory.StartNew(() => MalzemeFiyatTransferAsync(), CancellationToken.None, TaskCreationOptions.None, taskScheduler);
        }

        public async Task MalzemeTransfer()
        {
            while (true)
            {
                await transfer.MalzemeTransfer();

                if (_b2bSetting.VirtualStore == "B2B")
                {
                    await Task.Delay(1000 * 60 * 10);
                }

                if (_b2bSetting.VirtualStore == "Smartstore")
                {
                    await Task.Delay(1000 * 60 * _smartstoreParameter.ProductTransferMinute);
                }

            }
        }
        public async Task MalzemeStokTransfer()
        {
            while (true)
            {
                await transfer.MalzemeStokTransfer();

                if (_b2bSetting.VirtualStore == "B2B")
                {
                    await Task.Delay(1000 * 60 * 10);
                }

                if (_b2bSetting.VirtualStore == "Smartstore")
                {
                    await Task.Delay(1000 * 60 * _smartstoreParameter.ProductStockTransferMinute);
                }
            }
        }

        public async Task MalzemeFiyatTransferAsync()
        {
            while (true)
            {
                await transfer.MalzemeFiyatTransfer();

                if (_b2bSetting.VirtualStore == "B2B")
                {
                    await Task.Delay(1000 * 60 * 10);
                }

                if (_b2bSetting.VirtualStore == "Smartstore")
                {
                    await Task.Delay(1000 * 60 * _smartstoreParameter.ProductPriceTransferMinute);
                }
            }
        }
    }
}
