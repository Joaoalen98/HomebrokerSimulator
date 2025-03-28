namespace HomebrokerSimulator.ClientAPI.Infra.Messageria.DTOs;

public record class OrderQueueStatusDTO(
    string OrderId,
    string Status
)
{

}
