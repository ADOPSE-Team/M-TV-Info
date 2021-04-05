using MongoDB.Bson.Serialization.Attributes;
using System;

namespace API.Models
{
    public class MEDIA_LOOKUP_model
    { 
        [BsonId]
        public Guid ID { get; set; }

        public int MEDIA_ID { get; set; }

        public string NAME { get; set; }

        public string POSTER_URL { get; set; }
    }
}