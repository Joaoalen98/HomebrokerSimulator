using HomebrokerSimulator.ClientAPI.Features.Wallets.DTOs;
using HomebrokerSimulator.ClientAPI.Features.Wallets.Entities;

namespace HomebrokerSimulator.ClientAPI.Features.Wallets;
public interface IWalletService
{
    Task<Wallet> CreateWallet();
    Task<WalletAsset> CreateWalletAsset(CreateWalletAssetDTO createWalletAssetDTO);
    Task<Wallet> GetWalletById(string id);
    Task<IEnumerable<Wallet>> GetWallets();
}