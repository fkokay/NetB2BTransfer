using Microsoft.EntityFrameworkCore;
using NetTransferService;
using System.Runtime.Versioning;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Logging.ClearProviders();

// Ensure the following code is only executed on Windows platforms  
if (OperatingSystem.IsWindows())
{
    builder.Logging.AddEventLog();
}

builder.Services.AddWindowsService(opt =>
{
    opt.ServiceName = "NetTransferService";
});

IHost host = builder.Build();
host.Run();