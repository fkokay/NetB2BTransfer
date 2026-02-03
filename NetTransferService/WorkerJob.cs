using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NetTransfer.Core.Entities;
using NetTransfer.Data;
using NetTransfer.Integration;
using NetTransferService.Jobs;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransferService
{
    public class WorkerJob : BackgroundService
    {
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IJobFactory _jobFactory;
        private readonly ILogger<WorkerJob> _logger;

        public WorkerJob(ISchedulerFactory schedulerFactory, IJobFactory jobFactory, ILogger<WorkerJob> logger)
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

                IJobDetail jobCustomer = JobBuilder
                   .Create<CustomerSyncJob>()
                   .WithIdentity("customerSyncJob", "defaultGroup")
                   .Build();

                ITrigger triggerCustomer = TriggerBuilder
                    .Create()
                    .WithIdentity("customerSyncTrigger", "defaultGroup")
                    .StartNow()
                    .WithSimpleSchedule(x => x.WithIntervalInMinutes(2).RepeatForever())
                .Build();

                await scheduler.ScheduleJob(jobCustomer, triggerCustomer, stoppingToken);

                IJobDetail jobCustomerBalance = JobBuilder
                 .Create<CustomerBalanceSyncJob>()
                 .WithIdentity("customerBalanceSyncJob", "defaultGroup")
                 .Build();

                ITrigger triggerCustomerBalance = TriggerBuilder
                    .Create()
                    .WithIdentity("customerBalanceSyncTrigger", "defaultGroup")
                    .StartNow()
                    .WithSimpleSchedule(x => x.WithIntervalInMinutes(2).RepeatForever())
                .Build();

                await scheduler.ScheduleJob(jobCustomerBalance, triggerCustomerBalance, stoppingToken);

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


                IJobDetail jobPayment = JobBuilder
                .Create<PaymentJob>()
                .WithIdentity("paymentJob", "defaultGroup")
                .Build();

                ITrigger triggerPayment = TriggerBuilder
                    .Create()
                    .WithIdentity("paymentSyncTrigger", "defaultGroup")
                    .StartNow()
                    .WithSimpleSchedule(x => x.WithIntervalInMinutes(2).RepeatForever())
                .Build();

                await scheduler.ScheduleJob(jobPayment, triggerPayment, stoppingToken);


                await Task.Delay(Timeout.Infinite, stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while starting the worker job.");
            }
        }


    }
}
