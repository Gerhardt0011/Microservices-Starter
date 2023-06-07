using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Bson;
using MongoDbGenericRepository.Attributes;

namespace Identity.Service.Models;

[CollectionName("Roles")]
public class ApplicationRole : MongoIdentityRole<ObjectId>
{
    
}