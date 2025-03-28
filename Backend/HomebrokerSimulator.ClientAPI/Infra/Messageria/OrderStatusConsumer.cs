using System;
using HomebrokerSimulator.ClientAPI.Features.Orders;
using HomebrokerSimulator.ClientAPI.Features.Orders.Enums;
using HomebrokerSimulator.ClientAPI.Infra.Messageria.DTOs;
using MassTransit;

namespace HomebrokerSimulator.ClientAPI.Infra.Messageria;

public class OrderStatusConsumer(IOrderService orderService) : IConsumer<OrderQueueStatusDTO>
{
    public async Task Consume(ConsumeContext<OrderQueueStatusDTO> context)
    {
        await orderService.UpdateOrderStatus(context.Message.OrderId, Enum.Parse<EOrderStatus>(context.Message.Status));
    }
}
