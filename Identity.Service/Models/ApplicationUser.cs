using System.ComponentModel.DataAnnotations;
using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;

namespace Identity.Service.Models;

[CollectionName("Users")]
public class ApplicationUser : MongoIdentityUser<ObjectId>
{
    [Required]
    public string FirstName { get; set; } = null!;
    
    [Required]
    public string LastName { get; set; } = null!;
    
    [Required]
    public string Name { get; set; } = null!;
    
    [BsonRepresentation(BsonType.ObjectId)]
    public string CurrentTeam { get; set; } = null!;
}