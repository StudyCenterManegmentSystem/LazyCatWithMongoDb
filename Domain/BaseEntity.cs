using MongoDB.Bson.Serialization.Attributes;
namespace Domain;

public class BaseEntity
{
    [BsonId]
    public string Id { get; set; } = string.Empty;
}
