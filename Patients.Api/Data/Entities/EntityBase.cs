using MongoDB.Bson.Serialization.Attributes;

namespace Patients.Api.Data.Entities;

public abstract class EntityBase
{
    [BsonId]
    public Guid Id { get; set; }
}