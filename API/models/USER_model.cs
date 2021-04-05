using MongoDB.Bson.Serialization.Attributes;
using System;

namespace API.Models
{
    public class USER_model
    { 
        [BsonId]
        public Guid ID { get; set; }

        public string USERNAME { get; set; }

        public Guid ROLE_ID { get; set; }

        public string EMAIL { get; set; }

        public DateTime BIRTH { get; set; }

        public string COUNTRY { get; set; }

        public DateTime DATE_CREATED { get; set; }
    }
}