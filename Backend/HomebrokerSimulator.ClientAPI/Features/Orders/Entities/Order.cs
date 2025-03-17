using HomebrokerSimulator.ClientAPI.Features.Orders.Enums;
using HomebrokerSimulator.ClientAPI.Features.Shared;
using HomebrokerSimulator.ClientAPI.Features.Shared.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HomebrokerSimulator.ClientAPI.Features.Orders.Entities;

public class Order : EntityBase
{
    [BsonRepresentation(BsonType.ObjectId)]
    public required string AssetId { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public required string WalletId { get; set; }

    public required decimal Price { get; set; }

    public required int Shares { get; set; }

    public required EOrderType Type { get; set; }
    
    public EOrderStatus Status { get; set; } = EOrderStatus.PENDING;

    public ICollection<OrderTrade> Trades { get; set; } = default!;
}