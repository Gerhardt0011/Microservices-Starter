using System.ComponentModel.DataAnnotations;
using Common.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Identity.Service.Models;

public class RefreshToken : IModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;

    [Required]
    public DateTime ExpiryDate { get; set; }

    [Required]
    public DateTime CreationDate { get; set; }

    [Required]
    public bool Used { get; set; }

    [Required]
    public bool Invalidated { get; set; }

    public string JwtId { get; set; } = null!;

    public string UserId { get; set; } = null!;
}