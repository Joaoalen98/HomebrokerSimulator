using HomebrokerSimulator.ClientAPI.Common.Exceptions;
using HomebrokerSimulator.ClientAPI.Features.Assets.Entities;
using HomebrokerSimulator.ClientAPI.Features.Wallets.DTOs;
using HomebrokerSimulator.ClientAPI.Features.Wallets.Entities;
using HomebrokerSimulator.ClientAPI.Infra.Mongo;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HomebrokerSimulator.ClientAPI.Features.Wallets;

public class WalletService(MongoDBService mongoDBService) : IWalletService
{
    public async Task<Wallet> CreateWallet()
    {
        var wallet = new Wallet();
        await mongoDBService.Wallets.InsertOneAsync(wallet);
        return wallet;
    }

    public async Task<IEnumerable<Wallet>> GetWallets()
    {
        var wallets = await mongoDBService.Wallets
            .Find(_ => true)
            .ToListAsync();

        return wallets;
    }

    public async Task<Wallet> GetWalletById(string id)
    {
        var pipeline = new[]
        {
            new BsonDocument
            {
                { "$lookup", new BsonDocument
                    {
                        { "from", "WalletAssets" },
                        { "localField", "_id" },
                        { "foreignField", "WalletId" },
                        { "as", "WalletAssets" },
                        { "pipeline", new BsonArray
                            {
                                new BsonDocument
                                {
                                    { "$lookup", new BsonDocument
                                        {
                                            { "from", "Assets" },
                                            { "localField", "AssetId" },
                                            { "foreignField", "_id" },
                                            { "as", "Asset" }
                                        }
                                    }
                                },
                                new BsonDocument
                                {
                                    { "$unwind", "$Asset" }
                                }
                            }
                        }
                    }
                }
            }
        };

        var wallet = await mongoDBService.Wallets
            .Aggregate<Wallet>(pipeline)
            .FirstOrDefaultAsync();

        return wallet ?? throw new NotFoundException("Carteira não encontrada");
    }

    public async Task<WalletAsset> CreateWalletAsset(CreateWalletAssetDTO createWalletAssetDTO)
    {
        var walletAsset = new WalletAsset
        {
            AssetId = createWalletAssetDTO.AssetId,
            Price = createWalletAssetDTO.Price,
            Shares = createWalletAssetDTO.Shares,
            WalletId = createWalletAssetDTO.WalletId,
        };

        await mongoDBService.WalletAssets.InsertOneAsync(walletAsset);

        return walletAsset;
    }
}
