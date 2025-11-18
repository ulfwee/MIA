using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MyWebApi.Entities
{
    public enum Status
    {
        Confirmed,
        Pending,
        Cancelled,
        Completed  
    }

    public class Booking
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("user_id")]  
        public string UserId { get; set; } 

        [BsonElement("master_id")]
        public string MasterId { get; set; }  

        [BsonElement("date")]
        public DateTime Date { get; set; }  

        [BsonElement("service_details")]
        public string ServiceDetails { get; set; }

        [BsonElement("status")]
        [BsonRepresentation(BsonType.String)]
        public Status Status { get; set; } = Status.Pending;  

        [BsonElement("refreshToken")]
        public string? RefreshToken { get; set; }

        [BsonElement("refreshTokenExpiryTime")]
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}