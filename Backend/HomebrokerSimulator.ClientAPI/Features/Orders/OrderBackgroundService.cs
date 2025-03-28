using System;
using HomebrokerSimulator.ClientAPI.Features.Orders.Entities;
using HomebrokerSimulator.ClientAPI.Infra.Mongo;
using Microsoft.AspNetCore.SignalR;
using MongoDB.Driver;

namespace HomebrokerSimulator.ClientAPI.Features.Orders;

public class OrderBackgroundService(
    MongoDBService mongoDBService,
    IHubContext<OrderHub> hub
) : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.CompletedTask;
    }

    public async Task ListenOrderUpdate(CancellationToken cancellationToken)
    {
        var pipeline = new EmptyPipelineDefinition<ChangeStreamDocument<Order>>()
            .Match(change => change.OperationType == ChangeStreamOperationType.Update ||
                             change.OperationType == ChangeStreamOperationType.Replace);

        var cursor = await mongoDBService.Orders.WatchAsync(pipeline, cancellationToken: cancellationToken);

        await cursor.ForEachAsync(async change =>
        {
            var order = change.FullDocument;
            await hub.Clients.Group(order.Id!).SendAsync("OrderUpdate", order, cancellationToken);
        }, cancellationToken);
    }
}
