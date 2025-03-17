using HomebrokerSimulator.ClientAPI.Features.Assets;
using HomebrokerSimulator.ClientAPI.Features.Wallets;
using HomebrokerSimulator.ClientAPI.Infra.Mongo;
using HomebrokerSimulator.ClientAPI.Infra.Mongo.DTOs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.Configure<MongoDBConfigDTO>(builder.Configuration.GetSection("Mongo"));

builder.Services.AddSingleton<MongoDBService>();
builder.Services.AddSingleton<IAssetService, AssetService>();
builder.Services.AddSingleton<IWalletService, WalletService>();

var app = builder.Build();

app.MapAssetRoutes();
app.MapWalletRoutes();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();
