using System.Text.Json.Serialization;
using HomebrokerSimulator.ClientAPI.Common.Middlewares;
using HomebrokerSimulator.ClientAPI.Features.Assets;
using HomebrokerSimulator.ClientAPI.Features.Orders;
using HomebrokerSimulator.ClientAPI.Features.Wallets;
using HomebrokerSimulator.ClientAPI.Infra.Mongo;
using HomebrokerSimulator.ClientAPI.Infra.Mongo.DTOs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.Configure<MongoDBConfigDTO>(builder.Configuration.GetSection("Mongo"));
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddSingleton<MongoDBService>();
builder.Services.AddSingleton<IAssetService, AssetService>();
builder.Services.AddSingleton<IWalletService, WalletService>();
builder.Services.AddSingleton<IOrderService, OrderService>();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapAssetRoutes();
app.MapWalletRoutes();
app.MapOrderRoutes();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();
