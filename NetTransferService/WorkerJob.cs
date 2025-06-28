using Quartz.Impl;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetTransferService.Jobs;
using Quartz.Spi;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NetTransfer.Core.Entities;
using NetTransfer.Integration;

namespace NetTransferService
{
    public class WorkerJob : BackgroundService
    {
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IJobFactory _jobFactory;
        private readonly ILogger<Worker> _logger;

        public WorkerJob(ISchedulerFactory schedulerFactory, IJobFactory jobFactory, ILogger<Worker> logger)
        {
            _schedulerFactory = schedulerFactory;
            _jobFactory = jobFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                IScheduler scheduler = await _schedulerFactory.GetScheduler(stoppingToken);
                scheduler.JobFactory = _jobFactory;
                await scheduler.Start(stoppingToken);

                IJobDetail jobProduct = JobBuilder
                    .Create<ProductSyncJob>()
                    .WithIdentity("productSyncJob", "defaultGroup")
                    .Build();

                ITrigger triggerProduct = TriggerBuilder
                    .Create()
                    .WithIdentity("productSyncTrigger", "defaultGroup")
                    .StartNow()
                    .WithSimpleSchedule(x => x.WithIntervalInMinutes(2).RepeatForever())
                .Build();

                await scheduler.ScheduleJob(jobProduct, triggerProduct, stoppingToken);

                IJobDetail jobProductPrice = JobBuilder
                    .Create<ProductPriceSyncJob>()
                    .WithIdentity("productPriceSyncJob", "defaultGroup")
                    .Build();

                ITrigger triggerProductPrice = TriggerBuilder
                    .Create()
                    .WithIdentity("productPriceSyncTrigger", "defaultGroup")
                    .StartNow()
                    .WithSimpleSchedule(x => x.WithIntervalInMinutes(2).RepeatForever())
                .Build();

                await scheduler.ScheduleJob(jobProductPrice, triggerProductPrice, stoppingToken);

                IJobDetail jobProductStock = JobBuilder
                    .Create<ProductStockSyncJob>()
                    .WithIdentity("productStockSyncJob", "defaultGroup")
                    .Build();

                ITrigger triggerProductStock = TriggerBuilder
                    .Create()
                    .WithIdentity("productStockSyncTrigger", "defaultGroup")
                    .StartNow()
                    .WithSimpleSchedule(x => x.WithIntervalInMinutes(2).RepeatForever())
                .Build();

                await scheduler.ScheduleJob(jobProductStock, triggerProductStock, stoppingToken);


                IJobDetail jobOrder = JobBuilder
                    .Create<OrderSyncJob>()
                    .WithIdentity("orderSyncJob", "defaultGroup")
                    .Build();

                ITrigger triggerOrder = TriggerBuilder
                    .Create()
                    .WithIdentity("orderSyncTrigger", "defaultGroup")
                    .StartNow()
                    .WithSimpleSchedule(x => x.WithIntervalInMinutes(2).RepeatForever())
                .Build();

                await scheduler.ScheduleJob(jobOrder, triggerOrder, stoppingToken);

                IJobDetail jobShippment = JobBuilder
                .Create<ShipmentSyncJob>()
                .WithIdentity("shipmentSyncJob", "defaultGroup")
                .Build();

                ITrigger triggerShippment = TriggerBuilder
                    .Create()
                    .WithIdentity("shipmentSyncTrigger", "defaultGroup")
                    .StartNow()
                    .WithSimpleSchedule(x => x.WithIntervalInMinutes(2).RepeatForever())
                .Build();

                await scheduler.ScheduleJob(jobShippment, triggerShippment, stoppingToken);


                await Task.Delay(Timeout.Infinite, stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while starting the worker job.");
            }
        }

        
    }
}
