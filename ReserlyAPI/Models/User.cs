using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ReserlyAPI.Models
{
    public class User
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.Int32)]
        public required int Id { get; set; }

        [BsonElement("full_name"), BsonRepresentation(BsonType.String)]
        public string FullName { get; set; }

        [BsonElement("email"), BsonRepresentation(BsonType.String)]
        public string Email { get; set; }

        [BsonElement("password_hash"), BsonRepresentation(BsonType.String)]
        public string PasswordHash { get; set; }

        [BsonElement("role"), BsonRepresentation(BsonType.String)]
        public string Role { get; set; }
    }
}
