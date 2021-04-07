using MongoDB.Bson.Serialization.Attributes;
using System;

namespace API.Models
{
    public class RATING_model
    { 
        [BsonId]
        public Guid ID { get; set; }

        public Guid MEDIA_ID { get; set; }
        public Guid USER_ID { get; set; }

        public int RATE { get; set; }
        public DateTime DATE_CREATED { get; set; }
    }
    
}