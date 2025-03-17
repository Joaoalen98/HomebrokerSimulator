namespace HomebrokerSimulator.ClientAPI.Features.Assets.DTOs;

public record CreateAssetDTO(
    string Symbol,
    string Name,
    decimal Price)
{
}
