using HomebrokerSimulator.ClientAPI.Features.Assets.Entities;
using HomebrokerSimulator.ClientAPI.Features.Orders.Entities;
using HomebrokerSimulator.ClientAPI.Features.Wallets.Entities;
using HomebrokerSimulator.ClientAPI.Infra.Mongo.DTOs;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace HomebrokerSimulator.ClientAPI.Infra.Mongo;

public class MongoDBService
{
    public IMongoClient Client { get; private set; }

    public IMongoCollection<Asset> Assets { get; private set; }
    public IMongoCollection<AssetDaily> AssetDailies { get; private set; }
    public IMongoCollection<Order> Orders { get; private set; }
    public IMongoCollection<OrderTrade> OrderTrades { get; private set; }
    public IMongoCollection<Wallet> Wallets { get; private set; }
    public IMongoCollection<WalletAsset> WalletAssets { get; private set; }

    public MongoDBService(IOptions<MongoDBConfigDTO> options)
    {
        Client = new MongoClient(options.Value.Url);

        var database = Client.GetDatabase(options.Value.DatabaseName);

        Assets = database.GetCollection<Asset>("Assets");
        AssetDailies = database.GetCollection<AssetDaily>("AssetDailies");
        Orders = database.GetCollection<Order>("Orders");
        OrderTrades = database.GetCollection<OrderTrade>("OrderTrades");
        Wallets = database.GetCollection<Wallet>("Wallets");
        WalletAssets = database.GetCollection<WalletAsset>("WalletAssets");

        CreateIndexes();
    }

    public void CreateIndexes()
    {
        var options = new CreateIndexOptions { Unique = true };

        var indexAsset = new CreateIndexModel<Asset>(
                Builders<Asset>.IndexKeys
                    .Ascending(a => a.Symbol), options);

        var indexAssetDaily = new CreateIndexModel<AssetDaily>(
                Builders<AssetDaily>.IndexKeys
                    .Ascending(d => d.Date));

        var indexWalletAsset = new CreateIndexModel<WalletAsset>(
            Builders<WalletAsset>.IndexKeys
                .Ascending(wa => wa.WalletId)
                .Ascending(wa => wa.AssetId),
                options);

        Assets.Indexes.CreateOne(indexAsset);
        AssetDailies.Indexes.CreateOne(indexAssetDaily);
        WalletAssets.Indexes.CreateOne(indexWalletAsset);
    }
}
