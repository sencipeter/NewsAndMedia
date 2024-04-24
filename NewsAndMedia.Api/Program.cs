using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using NewsAndMedia.Core;
using NewsAndMedia.Core.Interfaces;
using NewsAndMedia.Infrastructure;
using NewsAndMedia.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<NewsAndMediaDbContext> (options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("AppConnectionString"))
);

builder.Services.Configure<RabbitMqConfiguration>(
    builder.Configuration.GetSection(RabbitMqConfiguration.Position));

builder.Services.AddSingleton<IMemoryCache, MemoryCache>();
builder.Services.AddSingleton<ICalculationService, CalculationService>();
builder.Services.AddScoped<IMessageClient, MessageClient>();

builder.Services.AddControllers();

var app = builder.Build();

using var scope = app.Services.CreateScope();
var dbcontext = scope.ServiceProvider.GetRequiredService<NewsAndMediaDbContext>();
dbcontext.Database.Migrate();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();