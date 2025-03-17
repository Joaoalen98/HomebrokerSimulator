using HomebrokerSimulator.ClientAPI.Features.Assets.DTOs;

namespace HomebrokerSimulator.ClientAPI.Features.Assets;

public static class AssetRoutes
{
    public static void MapAssetRoutes(this WebApplication app)
    {
        var group = app.MapGroup("/api/assets");

        group.MapPost("", async (IAssetService assetService, CreateAssetDTO createAssetDTO) =>
            await assetService.CreateAsset(createAssetDTO));

        group.MapGet("", async (IAssetService assetService) => await assetService.GetAssets());

        group.MapGet("{id}", async (IAssetService assetService, string id) => await assetService.GetAssetById(id));

        group.MapPost("dailies", async (IAssetService assetService, CreateAssetDailyDTO createAssetDailyDTO) =>
            await assetService.CreateAssetDaily(createAssetDailyDTO));

        group.MapGet("dailies/{id}", async (IAssetService assetService, string id) => await assetService.GetAssetDailies(id));
    }
}
