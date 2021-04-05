using MongoDB.Bson.Serialization.Attributes;
using System;

namespace API.Models
{
    public class WATCHLIST_model
    { 
        [BsonId]
        public Guid ID { get; set; }

        public Guid USER_ID { get; set; }

        public String NAME { get; set; }

        public DateTime DATE_CREATED { get; set; }
    }
}