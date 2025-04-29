using Microsoft.Extensions.Logging;
using NetB2BTransfer.B2B.Library;
using NetB2BTransfer.Core.Entities;
using NetB2BTransfer.Core.TaskSchedulers;
using NetB2BTransfer.Data;

namespace NetB2BTransferService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly NetB2BTransferContext _context;
        private readonly IConfiguration _configuration;

        private NetTaskScheduler taskScheduler = new NetTaskScheduler();
        private ErpSetting? _erpSetting;
        private B2BSetting? _b2bSetting;
        private LogoTransferSetting? _logoTransferSetting;
        private Transfer transfer;

        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _context = new NetB2BTransferContext(_configuration.GetConnectionString("DefaultConnection")!.ToString()); ;

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

            if (_erpSetting.Erp == "Logo")
            {
                _logoTransferSetting = _context.LogoTransferSetting.FirstOrDefault();
                if (_logoTransferSetting == null)
                {
                    _logger.LogInformation("Logo transfer ayarlarýný tanýmlayýn. Datetime : {time}", DateTimeOffset.Now);
                    return;
                }
            }
            _b2bSetting = _context.B2BSetting.FirstOrDefault();
            if (_b2bSetting == null)
            {
                _logger.LogInformation("B2B ayarlarýný tanýmlayýn. Datetime : {time}", DateTimeOffset.Now);
                return;
            }

            if (_erpSetting.Erp == "Logo")
            {
                transfer = new Transfer(_logger, _erpSetting, _b2bSetting, _logoTransferSetting);
            }
            else if (_erpSetting.Erp == "Netsis")
            {
                transfer = new Transfer(_logger, _erpSetting, _b2bSetting);
            }
            else
            {
                transfer = new Transfer(_logger, _erpSetting, _b2bSetting);
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Factory.StartNew(() => MalzemeTransfer(), CancellationToken.None, TaskCreationOptions.None, taskScheduler);
            await Task.Factory.StartNew(() => MalzemeStokTransfer(), CancellationToken.None, TaskCreationOptions.None, taskScheduler);
            await Task.Factory.StartNew(() => MalzemeFiyatTransferAsync(), CancellationToken.None, TaskCreationOptions.None, taskScheduler);
        }

        public async Task MalzemeFiyatTransferAsync()
        {
            while (true)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Malzeme Fiyat Aktarýmý Baþladý : {time}", DateTimeOffset.Now);
                }

                await transfer.MalzemeFiyatTransfer();

                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Malzeme Fiyat Aktarýmý Tamamlandý : {time}", DateTimeOffset.Now);
                }

                await Task.Delay(1000 * 60 * 5);
            }
        }
        public async Task MalzemeTransfer()
        {
            while (true)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Malzeme Aktarýmý Baþladý : {time}", DateTimeOffset.Now);
                }

                await transfer.MalzemeTransfer();

                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Malzeme Aktarýmý Tamamlandý : {time}", DateTimeOffset.Now);
                }

                await Task.Delay(1000 * 60 * 10);
            }
        }
        public async Task MalzemeStokTransfer()
        {
            while (true)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Malzeme Stok Aktarýmý Baþladý : {time}", DateTimeOffset.Now);
                }

                await transfer.MalzemeStokTransfer();

                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Malzeme Stok Aktarýmý Tamamlandý : {time}", DateTimeOffset.Now);
                }

                await Task.Delay(1000 * 60 * 5);
            }
        }
    }
}
