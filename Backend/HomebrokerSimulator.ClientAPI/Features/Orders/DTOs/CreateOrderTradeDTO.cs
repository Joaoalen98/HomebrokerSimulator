namespace HomebrokerSimulator.ClientAPI.Features.Orders.DTOs;

public record CreateOrderTradeDTO(
    string OrderId,
    decimal Price,
    int Shares)
{
}
