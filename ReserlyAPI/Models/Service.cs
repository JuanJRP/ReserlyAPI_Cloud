using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ReserlyAPI.Models
{
    public class Service
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.Int32)]
        public required int Id { get; set; }

        [BsonElement("name"), BsonRepresentation(BsonType.String)]
        public string Name { get; set; }

        [BsonElement("description"), BsonRepresentation(BsonType.String)]
        public string Description { get; set; }

        [BsonElement("price"), BsonRepresentation(BsonType.Decimal128)]
        public required decimal Price { get; set; }
    }
}
