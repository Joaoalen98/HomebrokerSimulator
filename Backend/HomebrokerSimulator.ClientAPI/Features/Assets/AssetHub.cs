using Microsoft.AspNetCore.SignalR;

namespace HomebrokerSimulator.ClientAPI.Features.Assets;

public class AssetHub : Hub
{
    public async Task JoinAssetGroup(string assetId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, assetId);
    }
}
