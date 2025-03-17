namespace HomebrokerSimulator.ClientAPI.Features.Assets.DTOs;

public record CreateAssetDailyDTO(
    string AssetId,
    DateTime Date,
    decimal Price)
{
}
