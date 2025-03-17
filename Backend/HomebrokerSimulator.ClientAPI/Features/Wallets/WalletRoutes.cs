using HomebrokerSimulator.ClientAPI.Features.Wallets.DTOs;

namespace HomebrokerSimulator.ClientAPI.Features.Wallets;

public static class WalletRoutes
{
    public static void MapWalletRoutes(this WebApplication app)
    {
        var group = app.MapGroup("/api/wallets");

        group.MapPost("", async (IWalletService walletService) =>
           await walletService.CreateWallet());

        group.MapGet("", async (IWalletService walletService) =>
            await walletService.GetWallets());

        group.MapGet("{id}", async (IWalletService walletService, string id) =>
            await walletService.GetWalletById(id));

        group.MapPost("wallet-assets", async (IWalletService walletService, CreateWalletAssetDTO createWalletAssetDTO) =>
            await walletService.CreateWalletAsset(createWalletAssetDTO));
    }
}
