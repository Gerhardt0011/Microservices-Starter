using Common.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Customers.Service.Models;

public class Team : IModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;
}