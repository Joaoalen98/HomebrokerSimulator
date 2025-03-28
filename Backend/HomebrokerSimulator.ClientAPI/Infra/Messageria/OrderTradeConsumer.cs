using System;
using HomebrokerSimulator.ClientAPI.Features.Orders;
using HomebrokerSimulator.ClientAPI.Infra.Messageria.DTOs;
using MassTransit;

namespace HomebrokerSimulator.ClientAPI.Infra.Messageria;

public class OrderTradeConsumer(IOrderService orderService) : IConsumer<OrderQueueTradeDTO>
{
    public async Task Consume(ConsumeContext<OrderQueueTradeDTO> context)
    {
        await orderService.CreateOrderTrade(new Features.Orders.DTOs.CreateOrderTradeDTO(
            context.Message.OrderId,
            context.Message.Price,
            context.Message.Shares
        ));
    }
}
