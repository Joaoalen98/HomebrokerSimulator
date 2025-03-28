namespace HomebrokerSimulator.ClientAPI.Features.Orders.DTOs;

public record class CreateQueueOrder(
    string AssetSymbol,
    decimal Price,
    int Shares,
    string Type,
    string Identifier
)
{

}
