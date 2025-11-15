using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyWebApi.Models
{
    public class TodoItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string FullTask { get; set; } = "";

        [BsonElement("is_complete")]
        public bool IsComplete { get; set; }
    }

    public class TodoItemDto
    {
        public string Id { get; set; }
        public string Task{ get; set; }
        public bool IsCompleted { get; set; }
    }
}