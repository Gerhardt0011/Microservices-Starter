using System.ComponentModel.DataAnnotations;
using Common.Enums.Teams;
using Common.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Identity.Service.Models;

public class Team : IModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;
    
    [Required]
    [BsonRepresentation(BsonType.ObjectId)]
    public string UserId { get; set; } = null!;

    [Required]
    public string Name { get; set; } = null!;
    
    [BsonRepresentation(BsonType.ObjectId)]
    public List<string> Members { get; set; } = null!;

    [Required]
    public TeamType Type { get; set; } = TeamType.Reseller;
}