namespace HomebrokerSimulator.ClientAPI.Features.Assets.DTOs;

public record CreateAssetDTO(
    string Symbol,
    decimal Price)
{
}
