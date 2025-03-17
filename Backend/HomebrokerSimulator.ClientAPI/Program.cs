using HomebrokerSimulator.ClientAPI.Features.Assets;
using HomebrokerSimulator.ClientAPI.Infra.Mongo;
using HomebrokerSimulator.ClientAPI.Infra.Mongo.DTOs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.Configure<MongoDBConfigDTO>(builder.Configuration.GetSection("Mongo"));

builder.Services.AddSingleton<MongoDBService>();
builder.Services.AddSingleton<IAssetService, AssetService>();

var app = builder.Build();

app.MapAssetRoutes();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();
