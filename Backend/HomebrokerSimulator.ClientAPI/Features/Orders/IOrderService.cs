using HomebrokerSimulator.ClientAPI.Features.Orders.DTOs;
using HomebrokerSimulator.ClientAPI.Features.Orders.Entities;
using HomebrokerSimulator.ClientAPI.Features.Orders.Enums;

namespace HomebrokerSimulator.ClientAPI.Features.Orders;
public interface IOrderService
{
    Task<Order> CreateOrder(CreateOrderDTO createOrderDTO);
    Task<OrderTrade> CreateOrderTrade(CreateOrderTradeDTO createOrderTradeDTO);
    Task<Order> GetOrderById(string orderId);
    Task<IEnumerable<Order>> GetOrdersByWalletId(string walletId);
    Task<Order> UpdateOrderStatus(string orderId, EOrderStatus orderStatus);
}