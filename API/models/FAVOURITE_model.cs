using MongoDB.Bson.Serialization.Attributes;
using System;

namespace API.Models
{
    public class FAVOURITE_model
    { 
        [BsonId]
        public Guid ID { get; set; }

        public Guid MEDIA_ID { get; set; }

        public Guid USER_ID { get; set; }

        public DateTime DATE_CREATED { get; set; }
    }

    
}