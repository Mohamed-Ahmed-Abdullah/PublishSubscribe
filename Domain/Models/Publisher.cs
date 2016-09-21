using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Models
{
    public class Publisher : ModelBase
    {
        public string Name { get; set; }

        public List<Message> PushedMessages { get; set; }
    }
}