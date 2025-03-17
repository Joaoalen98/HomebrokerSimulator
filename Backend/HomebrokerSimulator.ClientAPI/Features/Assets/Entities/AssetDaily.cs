using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HomebrokerSimulator.ClientAPI.Features.Assets.Entities;

public class AssetDaily
{
    [BsonRepresentation(BsonType.ObjectId)]
    public required string AssetId { get; set; }

    public required DateTime Date { get; set; } = DateTime.Now;
    
    public required decimal Price { get; set; }
}