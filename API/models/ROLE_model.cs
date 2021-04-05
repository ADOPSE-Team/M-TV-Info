using MongoDB.Bson.Serialization.Attributes;
using System;

namespace API.Models
{
    public class ROLE_model
    { 
        [BsonId]
        public Guid ID { get; set; }

        public string NAME { get; set; }

        public DateTime DATE_CREATED { get; set; }
    }
}