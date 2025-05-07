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
        private VirtualStoreSetting? _virtualStoreSetting;

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

            _virtualStoreSetting = _context.VirtualStoreSetting.FirstOrDefault();
            if (_virtualStoreSetting == null)
            {
                _logger.LogInformation("B2B ayarlarýný tanýmlayýn. Datetime : {time}", DateTimeOffset.Now);
                return;
            }

            if (_virtualStoreSetting.VirtualStore == "B2B")
            {
                _virtualStoreSetting = _context.VirtualStoreSetting.FirstOrDefault();
                transfer = new Transfer(_logger, _erpSetting, _virtualStoreSetting, _b2BParameter);
            }
            else if (_virtualStoreSetting.VirtualStore == "Smartstore")
            {
                _smartstoreParameter = _context.SmartstoreParameter.FirstOrDefault();
                transfer = new Transfer(_logger, _erpSetting, _virtualStoreSetting, _smartstoreParameter);
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Factory.StartNew(() => CariTransferAsync(), CancellationToken.None, TaskCreationOptions.None, taskScheduler);
            await Task.Factory.StartNew(() => CariBakiyeTransferAsync(), CancellationToken.None, TaskCreationOptions.None, taskScheduler);
            await Task.Factory.StartNew(() => MalzemeTransfer(), CancellationToken.None, TaskCreationOptions.None, taskScheduler);
            await Task.Factory.StartNew(() => MalzemeStokTransfer(), CancellationToken.None, TaskCreationOptions.None, taskScheduler);
            await Task.Factory.StartNew(() => MalzemeFiyatTransferAsync(), CancellationToken.None, TaskCreationOptions.None, taskScheduler);
            await Task.Factory.StartNew(() => SiparisTransferAsync(), CancellationToken.None, TaskCreationOptions.None, taskScheduler);
        }

        private async Task SiparisTransferAsync()
        {
            int siparisTransferMinute = getSiparisTransferMinute();


            if (siparisTransferMinute > 0)
            {
                while (true)
                {
                    await transfer.SiparisTransfer();

                    await Task.Delay(1000 * 60 * siparisTransferMinute);
                }
            }
        }

        private int getSiparisTransferMinute()
        {
            if (_virtualStoreSetting.VirtualStore == "B2B")
            {
                return _b2BParameter.OrderTransferMinute;
            }
            if (_virtualStoreSetting.VirtualStore == "Smartstore")
            {
                return 0;
            }

            return 0;
        }

        public async Task CariTransferAsync()
        {
            int customerTransferMinute = getCustomerTransferMinute();


            if (customerTransferMinute > 0)
            {
                while (true)
                {
                    await transfer.MalzemeFiyatTransfer();

                    await Task.Delay(1000 * 60 * customerTransferMinute);
                }
            }
        }

        private int getCustomerTransferMinute()
        {
            if (_virtualStoreSetting.VirtualStore == "B2B")
            {
                return _b2BParameter.CustomerTransferMinute;
            }
            if (_virtualStoreSetting.VirtualStore == "Smartstore")
            {
                return 0;
            }

            return 0;
        }

        public async Task CariBakiyeTransferAsync()
        {
            int customerBalanceTransferMinute = getCustomerBakiyeTransferMinute();


            if (customerBalanceTransferMinute > 0)
            {
                while (true)
                {
                    await transfer.CariBakiyeTransfer();

                    await Task.Delay(1000 * 60 * customerBalanceTransferMinute);
                }
            }
        }

        private int getCustomerBakiyeTransferMinute()
        {
            if (_virtualStoreSetting.VirtualStore == "B2B")
            {
                return _b2BParameter.CustomerTransferMinute;
            }
            if (_virtualStoreSetting.VirtualStore == "Smartstore")
            {
                return 0;
            }

            return 0;
        }

        public async Task MalzemeTransfer()
        {
            int productTransferMinute = getProductTransferMinute();
            if (productTransferMinute > 0)
            {
                while (true)
                {
                    await transfer.MalzemeTransfer();
                    await Task.Delay(1000 * 60 * productTransferMinute);
                }
            }
        }

        private int getProductTransferMinute()
        {
            if (_virtualStoreSetting.VirtualStore == "B2B")
            {
                return _b2BParameter.ProductTransferMinute;
            }
            if (_virtualStoreSetting.VirtualStore == "Smartstore")
            {
                return _smartstoreParameter.ProductTransferMinute;
            }

            return 0;
        }

        public async Task MalzemeStokTransfer()
        {
            int productStockTransferMinute = getProductStockTransferMinute();
            if (productStockTransferMinute > 0)
            {
                while (true)
                {
                    await transfer.MalzemeStokTransfer();

                    await Task.Delay(1000 * 60 * productStockTransferMinute);
                }
            }
         
        }

        private int getProductStockTransferMinute()
        {
            if (_virtualStoreSetting.VirtualStore == "B2B")
            {
                return _b2BParameter.ProductStockTransferMinute;
            }
            if (_virtualStoreSetting.VirtualStore == "Smartstore")
            {
                return _smartstoreParameter.ProductStockTransferMinute;
            }

            return 0;
        }

        public async Task MalzemeFiyatTransferAsync()
        {
            int productPriceTransferMinute = getProductPriceTransferMinute();
            if (productPriceTransferMinute > 0)
            {
                while (true)
                {
                    await transfer.MalzemeFiyatTransfer();
                    await Task.Delay(1000 * 60 * productPriceTransferMinute);
                }
            }
        }

        private int getProductPriceTransferMinute()
        {
            if (_virtualStoreSetting.VirtualStore == "B2B")
            {
                return _b2BParameter.ProductPriceTransferMinute;
            }
            if (_virtualStoreSetting.VirtualStore == "Smartstore")
            {
                return _smartstoreParameter.ProductPriceTransferMinute;
            }

            return 0;
        }

    }
}
