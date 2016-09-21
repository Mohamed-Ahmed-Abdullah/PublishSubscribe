using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Models
{
    public class Subscriber : ModelBase
    {
        public string Name { get; set; }

        public PullCriteria PullCriteria { get; set; }

        public List<Message> PulledMessages { get; set; }
    }
}