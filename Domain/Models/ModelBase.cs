using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Models
{
    public class ModelBase
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonIgnore]
        public string StringId { get; set; }
    }
}