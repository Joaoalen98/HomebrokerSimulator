using System.Text.Json.Serialization;
using HomebrokerSimulator.ClientAPI.Common.Middlewares;
using HomebrokerSimulator.ClientAPI.Features.Assets;
using HomebrokerSimulator.ClientAPI.Features.Orders;
using HomebrokerSimulator.ClientAPI.Features.Wallets;
using HomebrokerSimulator.ClientAPI.Infra.Messageria;
using HomebrokerSimulator.ClientAPI.Infra.Mongo;
using HomebrokerSimulator.ClientAPI.Infra.Mongo.DTOs;
using MassTransit;
using Microsoft.AspNetCore.Cors.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var cors = new CorsPolicyBuilder()
    .WithOrigins("http://localhost:4200")
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
    .Build();

builder.Services.AddCors(setup =>
{
    setup.AddDefaultPolicy(cors);
});

builder.Services.AddOpenApi();

builder.Services.Configure<MongoDBConfigDTO>(builder.Configuration.GetSection("Mongo"));
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddSignalR()
    .AddJsonProtocol(configure =>
    {
        configure.PayloadSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddMassTransit(config =>
{

    config.AddConsumer<AssetDailyCreateConsumer>();
    config.AddConsumer<OrderStatusConsumer>();
    config.AddConsumer<OrderTradeConsumer>();

    config.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq://localhost:5672", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("daily-create", e => e.ConfigureConsumer<AssetDailyCreateConsumer>(context));
        cfg.ReceiveEndpoint("order-status", e => e.ConfigureConsumer<OrderStatusConsumer>(context));
        cfg.ReceiveEndpoint("trade-create", e => e.ConfigureConsumer<OrderTradeConsumer>(context));
    });
});

builder.Services.AddHostedService<AssetBackgroundService>();
builder.Services.AddHostedService<OrderBackgroundService>();

builder.Services.AddSingleton<MongoDBService>();
builder.Services.AddSingleton<IAssetService, AssetService>();
builder.Services.AddSingleton<IWalletService, WalletService>();
builder.Services.AddSingleton<IOrderService, OrderService>();


var app = builder.Build();

app.UseCors();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapHub<AssetHub>("/asset-hub");
app.MapHub<OrderHub>("/order-hub");

app.MapAssetRoutes();
app.MapWalletRoutes();
app.MapOrderRoutes();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();
