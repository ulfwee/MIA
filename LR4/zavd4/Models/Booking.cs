using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyWebApi.Models
{
    public enum Status
    {
        Confirmed,
        Pending,
        Cancelled,
        Competed
    }
    public class Booking
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("master_id")]
        public int MasterId { get; set; }

        [BsonElement("date")]
        public string Date { get; set; }

        [BsonElement("service_details")]
        public string ServiceDetails { get; set; }

        [BsonElement("status")]
        [BsonRepresentation(BsonType.String)]
        public Status Status { get; set; }
    }
}
