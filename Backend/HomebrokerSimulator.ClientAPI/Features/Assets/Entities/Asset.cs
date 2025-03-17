using HomebrokerSimulator.ClientAPI.Features.Shared;
using HomebrokerSimulator.ClientAPI.Features.Shared.Entities;

namespace HomebrokerSimulator.ClientAPI.Features.Assets.Entities;

public class Asset : EntityBase
{
    public required string Symbol { get; set; }
    public required string Name { get; set; }
    public required decimal Price { get; set; }
}