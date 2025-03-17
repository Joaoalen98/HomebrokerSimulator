using HomebrokerSimulator.ClientAPI.Features.Shared;
using HomebrokerSimulator.ClientAPI.Features.Shared.Entities;

namespace HomebrokerSimulator.ClientAPI.Features.Wallets.Entities;

public class Wallet : EntityBase
{
    public ICollection<WalletAsset> WalletAssets { get; set; } = default!;
}