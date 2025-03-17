using HomebrokerSimulator.ClientAPI.Features.Shared;
using HomebrokerSimulator.ClientAPI.Features.Shared.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HomebrokerSimulator.ClientAPI.Features.Orders.Entities;

public class OrderTrade : EntityBase
{
    [BsonRepresentation(BsonType.ObjectId)]
    public required string OrderId { get; set; }

    public required int Shares { get; set; }

    public required decimal Price { get; set; }
}