using HomebrokerSimulator.ClientAPI.Features.Orders.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HomebrokerSimulator.ClientAPI.Features.Orders;

public static class OrderRoutes
{
    public static void MapOrderRoutes(this WebApplication app)
    {
        var group = app.MapGroup("/api/orders");

        group.MapPost("", async (IOrderService orderService, CreateOrderDTO createOrderDTO) =>
            await orderService.CreateOrder(createOrderDTO));

        group.MapGet("", async (IOrderService orderService, [FromQuery] string walletId) =>
            await orderService.GetOrdersByWalletId(walletId));

        group.MapGet("{orderId}", async (IOrderService orderService, string orderId) =>
            await orderService.GetOrderById(orderId));

        group.MapPost("trades", async (IOrderService orderService, CreateOrderTradeDTO createOrderTradeDTO) =>
            await orderService.CreateOrderTrade(createOrderTradeDTO));
    }
}
