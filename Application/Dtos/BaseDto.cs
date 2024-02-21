using MongoDB.Bson.Serialization.Attributes;

namespace Application.Dtos;

public abstract class BaseDto
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;
}
