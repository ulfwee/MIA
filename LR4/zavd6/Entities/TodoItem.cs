using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyWebApi.Entities
{
    public class TodoItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("user_id")]  // Додав для зв'язку з користувачем
        public string UserId { get; set; }

        [BsonElement("task")]
        public string Task { get; set; } = "";  // Перейменував з "FullTask" для простоти

        [BsonElement("is_complete")]
        public bool IsComplete { get; set; } = false;

        [BsonElement("refreshToken")]
        public string? RefreshToken { get; set; }

        [BsonElement("refreshTokenExpiryTime")]
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}