using Microsoft.EntityFrameworkCore;
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
        private readonly IConfiguration _configuration;
        private readonly NetTransferContext _context;

        private NetTaskScheduler taskScheduler = new NetTaskScheduler();
        private ErpSetting _erpSetting;
        private VirtualStoreSetting _virtualStoreSetting;
        private Transfer transfer;
        private B2BParameter _b2BParameter;
        private SmartstoreParameter _smartstoreParameter;

        private System.Timers.Timer timerCariTransfer;
        private System.Timers.Timer timerCariBakiyeTransfer;
        private System.Timers.Timer timerMalzemeTransfer;
        private System.Timers.Timer timerMalzemeStokTransfer;
        private System.Timers.Timer timerMalzemeFiyatTransfer;
        private System.Timers.Timer timerSiparisTransfer;
        private System.Timers.Timer timerSevkiyatTransfer;

        //    private Transfer transfer;
        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _context = new NetTransferContext(_configuration.GetConnectionString("DefaultConnection")!.ToString());
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }
        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogWarning("Net Transfer Baþladý");
            await InitializeSettingAsync();

            if (_virtualStoreSetting.VirtualStore == "B2B")
            {
                if (_b2BParameter == null)
                {
                    return;
                }
                _b2BParameter.CustomerLastTransfer = null;
                _b2BParameter.ProductLastTransfer = null;
                _b2BParameter.ProductStockLastTransfer = null;
                _b2BParameter.ProductPriceLastTransfer = null;
                _context.B2BParameter.Update(_b2BParameter);
                _context.SaveChanges();
            }

            if (_virtualStoreSetting.VirtualStore == "Smartstore")
            {
                //if (_smartstoreParameter == null)
                //{
                //    return;
                //}

                //_smartstoreParameter.ProductLastTransfer = null;
                //_smartstoreParameter.ProductStockLastTransfer = null;
                //_smartstoreParameter.ProductPriceLastTransfer = null;
                //_context.SmartstoreParameter.Update(_smartstoreParameter);
                //_context.SaveChanges();
            }

            if (getCustomerTransferMinute() > 0)
            {
                timerCariTransfer = new System.Timers.Timer();
                timerCariTransfer.Interval = 60000 * getCustomerTransferMinute();
                timerCariTransfer.Elapsed += TimerCariTransfer_Elapsed; ;
                timerCariTransfer.Enabled = true;
                timerCariTransfer.Start();

                TimerCariTransfer_Elapsed(this, new System.Timers.ElapsedEventArgs(DateTime.Now));
            }

            if (getCustomerBakiyeTransferMinute() > 0)
            {
                timerCariBakiyeTransfer = new System.Timers.Timer();
                timerCariBakiyeTransfer.Interval = 60000 * getCustomerBakiyeTransferMinute();
                timerCariBakiyeTransfer.Elapsed += TimerCariBakiyeTransfer_Elapsed; ;
                timerCariBakiyeTransfer.Enabled = true;
                timerCariBakiyeTransfer.Start();

                TimerCariBakiyeTransfer_Elapsed(this, new System.Timers.ElapsedEventArgs(DateTime.Now));
            }

            if (getProductTransferMinute() > 0)
            {
                timerMalzemeTransfer = new System.Timers.Timer();
                timerMalzemeTransfer.Interval = 60000 * getProductTransferMinute();
                timerMalzemeTransfer.Elapsed += TimerMalzemeTransfer_Elapsed;
                timerMalzemeTransfer.Enabled = true;
                timerMalzemeTransfer.Start();

                TimerMalzemeTransfer_Elapsed(this, new System.Timers.ElapsedEventArgs(DateTime.Now));
            }

            if (getProductStockTransferMinute() > 0)
            {
                timerMalzemeStokTransfer = new System.Timers.Timer();
                timerMalzemeStokTransfer.Interval = 60000 * getProductStockTransferMinute();
                timerMalzemeStokTransfer.Elapsed += TimerMalzemeStokTransfer_Elapsed;
                timerMalzemeStokTransfer.Enabled = true;
                timerMalzemeStokTransfer.Start();

                TimerMalzemeStokTransfer_Elapsed(this, new System.Timers.ElapsedEventArgs(DateTime.Now));
            }

            if (getProductPriceTransferMinute() > 0)
            {
                timerMalzemeFiyatTransfer = new System.Timers.Timer();
                timerMalzemeFiyatTransfer.Interval = 60000 * getProductPriceTransferMinute();
                timerMalzemeFiyatTransfer.Elapsed += TimerMalzemeFiyatTransfer_Elapsed;
                timerMalzemeFiyatTransfer.Enabled = true;
                timerMalzemeFiyatTransfer.Start();

                TimerMalzemeFiyatTransfer_Elapsed(this, new System.Timers.ElapsedEventArgs(DateTime.Now));
            }

            if (getSiparisTransferMinute() > 0)
            {
                timerSiparisTransfer = new System.Timers.Timer();
                timerSiparisTransfer.Interval = 60000 * getSiparisTransferMinute();
                timerSiparisTransfer.Elapsed += TimerSiparisTransfer_Elapsed;
                timerSiparisTransfer.Enabled = true;
                timerSiparisTransfer.Start();

                TimerSiparisTransfer_Elapsed(this, new System.Timers.ElapsedEventArgs(DateTime.Now));
            }

            if (getSevkiyatTransferMinute() > 0)
            {
                timerSevkiyatTransfer = new System.Timers.Timer();
                timerSevkiyatTransfer.Interval = 60000 * getSiparisTransferMinute();
                timerSevkiyatTransfer.Elapsed += TimerSevkiyatTransfer_Elapsed; ;
                timerSevkiyatTransfer.Enabled = true;
                timerSevkiyatTransfer.Start();

                TimerSevkiyatTransfer_Elapsed(this, new System.Timers.ElapsedEventArgs(DateTime.Now));
            }
        }

        private void TimerSevkiyatTransfer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            _logger.LogWarning("TimerSevkiyatTransfer_Elapsed");
            Task.Factory.StartNew(() => transfer.SevkiyatTransfer(), CancellationToken.None, TaskCreationOptions.None, taskScheduler);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            if (timerCariTransfer != null)
                timerCariTransfer.Dispose();
            if (timerCariBakiyeTransfer != null)
                timerCariBakiyeTransfer.Dispose();
            if (timerMalzemeTransfer != null)
                timerMalzemeTransfer.Dispose();
            if (timerMalzemeStokTransfer != null)
                timerMalzemeStokTransfer.Dispose();
            if (timerMalzemeFiyatTransfer != null)
                timerMalzemeFiyatTransfer.Dispose();
            if (timerSiparisTransfer != null)
                timerSiparisTransfer.Dispose();
            if (timerSevkiyatTransfer != null)
                timerSevkiyatTransfer.Dispose();

            _logger.LogWarning("Net Transfer Durduruldu");
            return base.StopAsync(cancellationToken);
        }

        private void TimerSiparisTransfer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            _logger.LogWarning("TimerSiparisTransfer_Elapsed");
            Task.Factory.StartNew(() => transfer.SiparisTransfer(), CancellationToken.None, TaskCreationOptions.None, taskScheduler);
        }

        private void TimerMalzemeFiyatTransfer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            _logger.LogWarning("TimerMalzemeFiyatTransfer_Elapsed");
            Task.Factory.StartNew(() => transfer.MalzemeFiyatTransfer(), CancellationToken.None, TaskCreationOptions.None, taskScheduler);
        }

        private void TimerMalzemeStokTransfer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            _logger.LogWarning("TimerMalzemeStokTransfer_Elapsed");
            Task.Factory.StartNew(() => transfer.MalzemeStokTransfer(), CancellationToken.None, TaskCreationOptions.None, taskScheduler);
        }

        private void TimerMalzemeTransfer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            _logger.LogWarning("TimerMalzemeTransfer_Elapsed");
            Task.Factory.StartNew(() => transfer.MalzemeTransfer(), CancellationToken.None, TaskCreationOptions.None, taskScheduler);
        }

        private void TimerCariBakiyeTransfer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            _logger.LogWarning("TimerCariBakiyeTransfer_Elapsed");
            Task.Factory.StartNew(() => transfer.CariBakiyeTransfer(), CancellationToken.None, TaskCreationOptions.None, taskScheduler);
        }

        private void TimerCariTransfer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            _logger.LogWarning("TimerCariTransfer_Elapsed");
            Task.Factory.StartNew(() => transfer.CariTransfer(), CancellationToken.None, TaskCreationOptions.None, taskScheduler);
        }

        public async Task InitializeSettingAsync()
        {
            try
            {
                _logger.LogWarning("Net Transfer Initialize Setting");


                if (!await _context.ErpSetting.AnyAsync())
                {
                    _logger.LogInformation("Erp ayarlarýný tanýmlayýn. Datetime : {time}", DateTimeOffset.Now);
                    return;
                }

                _erpSetting = _context.ErpSetting.First();


                if (!await _context.VirtualStoreSetting.AnyAsync())
                {
                    _logger.LogInformation("B2B ayarlarýný tanýmlayýn. Datetime : {time}", DateTimeOffset.Now);
                    return;
                }

                _virtualStoreSetting = await _context.VirtualStoreSetting.FirstAsync();

                string connectionString = _configuration.GetConnectionString("DefaultConnection")!.ToString();
                if (_virtualStoreSetting.VirtualStore == "B2B")
                {
                    _b2BParameter = await _context.B2BParameter.FirstAsync();
                    transfer = new Transfer(_logger, connectionString, _erpSetting, _virtualStoreSetting, _b2BParameter);
                }
                else if (_virtualStoreSetting.VirtualStore == "Smartstore")
                {
                    _smartstoreParameter = await _context.SmartstoreParameter.FirstAsync();
                    transfer = new Transfer(_logger, connectionString, _erpSetting, _virtualStoreSetting, _smartstoreParameter);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
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
        private int getSiparisTransferMinute()
        {
            if (_virtualStoreSetting.VirtualStore == "B2B")
            {
                return _b2BParameter.OrderTransferMinute;
            }
            if (_virtualStoreSetting.VirtualStore == "Smartstore")
            {
                return _smartstoreParameter.OrderTransferMinute;
            }

            return 0;
        }

        private int getSevkiyatTransferMinute()
        {
            if (_virtualStoreSetting.VirtualStore == "B2B")
            {
                return 0;
            }
            if (_virtualStoreSetting.VirtualStore == "Smartstore")
            {
                return _smartstoreParameter.OrderShipmentMinute;
            }

            return 0;
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
    }

}