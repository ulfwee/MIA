using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MyWebApi.Models;

namespace MyWebApi.Entities
{
    public enum Category
    {
        Plumbing,
        Electrical,
        Flooring,
        Assembly
    }

    public class Master
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("full_name")]
        public string FullName { get; set; }

        [BsonElement("category")]
        [BsonRepresentation(BsonType.String)]
        public Category Category { get; set; }

        [BsonElement("ranking")]
        public double Ranking { get; set; } = 0.0;  // За замовчуванням 0

        [BsonElement("role")]
        public Roles Role { get; set; } = Roles.Manager;

        [BsonElement("refreshToken")]
        public string? RefreshToken { get; set; }

        [BsonElement("refreshTokenExpiryTime")]
        public DateTime? RefreshTokenExpiryTime { get; set; }

    }
}