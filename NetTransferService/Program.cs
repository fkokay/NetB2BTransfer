using Microsoft.EntityFrameworkCore;
using NetTransfer.Data;
using NetTransferService;

var builder = Host.CreateApplicationBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddEventLog();

builder.Services.AddWindowsService(opt =>
{
    opt.ServiceName = "NetTransferService";  
});

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
