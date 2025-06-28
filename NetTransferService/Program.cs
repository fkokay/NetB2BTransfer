using Microsoft.Extensions.Options;
using NetTransferService;
using NetTransferService.Jobs;
using Quartz;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services.AddScoped<ProductSyncJob>();
builder.Services.AddScoped<CustomerBalanceSyncJob>();
builder.Services.AddScoped<CustomerSyncJob>();
builder.Services.AddScoped<OrderSyncJob>();
builder.Services.AddScoped<ProductPriceSyncJob>();
builder.Services.AddScoped<ProductStockSyncJob>();
builder.Services.AddScoped<ShipmentSyncJob>();

builder.Services.AddHostedService<WorkerJob>();
builder.Logging.ClearProviders();

if (OperatingSystem.IsWindows())
{
    builder.Logging.AddEventLog();
}

builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();
    q.UseSimpleTypeLoader();
    q.UseInMemoryStore();
});

builder.Services.AddWindowsService(opt =>
{
    opt.ServiceName = "NetTransferService";
});

IHost host = builder.Build();
host.Run();