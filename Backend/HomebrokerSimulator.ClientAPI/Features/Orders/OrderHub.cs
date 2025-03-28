using System;
using Microsoft.AspNetCore.SignalR;

namespace HomebrokerSimulator.ClientAPI.Features.Orders;

public class OrderHub : Hub
{
    public async Task JoinOrderRoom(string orderId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, orderId);
    }
}
