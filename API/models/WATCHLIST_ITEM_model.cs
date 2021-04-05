using MongoDB.Bson.Serialization.Attributes;
using System;

namespace API.Models
{
    public class WATCHLIST_ITEM_model
    { 
        [BsonId]
        public Guid ID { get; set; }

        public Guid WATCHLIST_ID { get; set; }

        public Guid MEDIA_ID { get; set; }

        public DateTime DATE_CREATED { get; set; }
    }
}