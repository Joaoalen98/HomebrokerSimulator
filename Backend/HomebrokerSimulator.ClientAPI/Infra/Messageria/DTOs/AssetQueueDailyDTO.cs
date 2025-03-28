namespace HomebrokerSimulator.ClientAPI.Infra.Messageria.DTOs;

public record class AssetQueueDailyDTO(
    string AssetSymbol,
    DateTime Date,
    decimal Price
)
{

}
