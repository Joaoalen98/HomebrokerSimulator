
using HomebrokerSimulator.ClientAPI.Features.Assets.Entities;
using HomebrokerSimulator.ClientAPI.Infra.Mongo;
using Microsoft.AspNetCore.SignalR;
using MongoDB.Driver;

namespace HomebrokerSimulator.ClientAPI.Features.Assets;

public class AssetBackgroundService(
    MongoDBService mongoDBService,
    IHubContext<AssetHub> hub) : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Task.Run(() => ListenAssetUpdate(stoppingToken), stoppingToken);
        return Task.CompletedTask;
    }

    public async Task ListenAssetUpdate(CancellationToken cancellationToken)
    {
        var pipeline = new EmptyPipelineDefinition<ChangeStreamDocument<Asset>>()
            .Match(change => change.OperationType == ChangeStreamOperationType.Update ||
                    change.OperationType == ChangeStreamOperationType.Insert ||
                    change.OperationType == ChangeStreamOperationType.Replace);

        var cursor = await mongoDBService.Assets.WatchAsync(pipeline, cancellationToken: cancellationToken);

        await cursor.ForEachAsync(async change =>
        {
            var asset = change.FullDocument;
            await hub.Clients.Group(asset.Id!).SendAsync("AssetUpdated", asset, cancellationToken);
        }, cancellationToken);
    }

    public async Task ListenAssetDailyInsert(CancellationToken cancellationToken)
    {
        var pipeline = new EmptyPipelineDefinition<ChangeStreamDocument<AssetDaily>>()
            .Match(change => change.OperationType == ChangeStreamOperationType.Insert);

        var cursor = await mongoDBService.AssetDailies.WatchAsync(pipeline, cancellationToken: cancellationToken);

        await cursor.ForEachAsync(async change =>
        {
            var asset = change.FullDocument;
            await hub.Clients.Group(asset.Id!).SendAsync("AssetDailyInsert", asset, cancellationToken);
        }, cancellationToken);
    }
}
