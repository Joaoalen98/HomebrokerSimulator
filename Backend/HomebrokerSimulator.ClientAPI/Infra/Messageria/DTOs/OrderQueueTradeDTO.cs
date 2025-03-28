namespace HomebrokerSimulator.ClientAPI.Infra.Messageria.DTOs;

public record class OrderQueueTradeDTO(
    string OrderId,
    decimal Price,
    int Shares
)
{

}
