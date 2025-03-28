using HomebrokerSimulator.ClientAPI.Common.Exceptions;
using HomebrokerSimulator.ClientAPI.Features.Assets.DTOs;
using HomebrokerSimulator.ClientAPI.Features.Assets.Entities;
using HomebrokerSimulator.ClientAPI.Infra.Mongo;
using MongoDB.Driver;

namespace HomebrokerSimulator.ClientAPI.Features.Assets;

public class AssetService(MongoDBService mongoDBService) : IAssetService
{
    public async Task<Asset> CreateAsset(CreateAssetDTO createAssetDTO)
    {
        var asset = new Asset
        {
            Price = createAssetDTO.Price,
            Symbol = createAssetDTO.Symbol,
            Name = createAssetDTO.Name
        };

        await mongoDBService.Assets.InsertOneAsync(asset);

        return asset;
    }

    public async Task<IEnumerable<Asset>> GetAssets()
    {
        var assets = await mongoDBService.Assets
            .Find(_ => true)
            .ToListAsync();

        return assets;
    }

    public async Task<Asset> GetAssetById(string id)
    {
        var asset = await mongoDBService.Assets
            .Find(a => a.Id == id)
            .FirstOrDefaultAsync();

        return asset ?? throw new NotFoundException("Ativo não encontrado");
    }

    public async Task<Asset> UpdateAssetPrice(string id, decimal price)
    {
        var update = Builders<Asset>.Update
            .Set(a => a.Price, price);

        var result = await mongoDBService.Assets
            .FindOneAndUpdateAsync(a => a.Id == id, update);

        return result;
    }

    public async Task<AssetDaily> CreateAssetDaily(CreateAssetDailyDTO createAssetDailyDTO)
    {
        var daily = new AssetDaily
        {
            AssetId = createAssetDailyDTO.AssetId,
            Date = createAssetDailyDTO.Date,
            Price = createAssetDailyDTO.Price
        };

        await mongoDBService.AssetDailies.InsertOneAsync(daily);

        return daily;
    }

    public async Task<IEnumerable<AssetDaily>> GetAssetDailies(string assetId)
    {
        var dailies = await mongoDBService.AssetDailies
            .Find(d => d.AssetId == assetId)
            .ToListAsync();

        return dailies;
    }

    public Task<Asset> GetAssetBySymbol(string id)
    {
        return mongoDBService.Assets
            .Find(a => a.Symbol == id)
            .FirstOrDefaultAsync();
    }
}
