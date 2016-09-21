using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Models
{
    public class Message : ModelBase
    {
        public string Title { get; set; }
        public List<string> Tags { get; set; }
        public string Content { get; set; }
    }
}