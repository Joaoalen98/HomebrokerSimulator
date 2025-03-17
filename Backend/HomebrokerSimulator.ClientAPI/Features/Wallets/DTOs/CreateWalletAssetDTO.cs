namespace HomebrokerSimulator.ClientAPI.Features.Wallets.DTOs;

public record CreateWalletAssetDTO(
    string WalletId,
    string AssetId,
    decimal Price,
    int Shares)
{
}
