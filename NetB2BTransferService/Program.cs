using Microsoft.EntityFrameworkCore;
using NetB2BTransfer.Data;
using NetB2BTransferService;

var builder = Host.CreateApplicationBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddEventLog();

builder.Services.AddWindowsService(opt =>
{
    opt.ServiceName = "NetB2BTransferService";  
});

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
