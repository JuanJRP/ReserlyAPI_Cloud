using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ReserlyAPI.Models
{
    public class Reservation
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.Int32)]
        public required int Id { get; set; }

        [BsonElement("reservation"), BsonRepresentation(BsonType.DateTime)]
        public DateTime ReservationDate { get; set; }

        [BsonElement("service_id"), BsonRepresentation(BsonType.Int64)]
        public int ServiceId { get; set; }

        [BsonElement("user_id"), BsonRepresentation(BsonType.Int64)]
        public int UserId { get; set; }

        [BsonElement("status"), BsonRepresentation(BsonType.String)]
        public string Status { get; set; }
    }
}
