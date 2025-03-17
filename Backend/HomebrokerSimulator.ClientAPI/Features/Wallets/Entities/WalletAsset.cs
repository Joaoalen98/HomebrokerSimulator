using HomebrokerSimulator.ClientAPI.Features.Assets.Entities;
using HomebrokerSimulator.ClientAPI.Features.Shared;
using HomebrokerSimulator.ClientAPI.Features.Shared.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HomebrokerSimulator.ClientAPI.Features.Wallets.Entities;

public class WalletAsset : EntityBase
{
    [BsonRepresentation(BsonType.ObjectId)]
    public required string WalletId { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public required string AssetId { get; set; }

    public Asset Asset { get; set; } = default!;
}