using NewsAndMedia.Core.Interfaces;
using NewsAndMedia.Infrastructure.Services;
using NewsAndMedia.WorkerService;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddSingleton<IMessageClient, MessageClient>();
var host = builder.Build();
host.Run();
