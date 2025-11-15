using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyWebApi.Models
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

        [BsonElement("name")]
        public string FullName { get; set; }

        [BsonElement("category")]
        [BsonRepresentation(BsonType.String)]
        public Category Category { get; set; }

        [BsonElement("ranking")]
        public double Ranking { get; set; }
    }

    public class MasterDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public double Details { get; set; }
    }
}
