using System.ComponentModel.DataAnnotations;
using Common.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Common.Enums.Teams;

namespace Teams.Service.Models;

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

    [Required]
    public TeamType Type { get; set; }

    public List<User> Members { get; set; } = new List<User>();
}