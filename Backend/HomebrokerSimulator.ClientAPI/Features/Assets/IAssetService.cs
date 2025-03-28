using HomebrokerSimulator.ClientAPI.Features.Assets.DTOs;
using HomebrokerSimulator.ClientAPI.Features.Assets.Entities;

namespace HomebrokerSimulator.ClientAPI.Features.Assets;
public interface IAssetService
{
    Task<Asset> CreateAsset(CreateAssetDTO createAssetDTO);
    Task<AssetDaily> CreateAssetDaily(CreateAssetDailyDTO createAssetDailyDTO);
    Task<Asset> GetAssetById(string id);
    Task<Asset> GetAssetBySymbol(string id);
    Task<IEnumerable<AssetDaily>> GetAssetDailies(string assetId);
    Task<IEnumerable<Asset>> GetAssets();
    Task<Asset> UpdateAssetPrice(string id, decimal price);
}