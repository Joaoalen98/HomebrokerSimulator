using HomebrokerSimulator.ClientAPI.Features.Orders.Enums;

namespace HomebrokerSimulator.ClientAPI.Features.Orders.DTOs;

public record CreateOrderDTO(
    string AssetId,
    string WalletId,
    decimal Price,
    int Shares,
    EOrderType Type)
{
}
