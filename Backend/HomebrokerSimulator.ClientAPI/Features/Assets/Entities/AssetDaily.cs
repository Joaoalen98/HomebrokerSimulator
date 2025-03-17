using HomebrokerSimulator.ClientAPI.Features.Shared.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HomebrokerSimulator.ClientAPI.Features.Assets.Entities;

public class AssetDaily : EntityBase
{
    [BsonRepresentation(BsonType.ObjectId)]
    public required string AssetId { get; set; }

    public required DateTime Date { get; set; }
    
    public required decimal Price { get; set; }
}