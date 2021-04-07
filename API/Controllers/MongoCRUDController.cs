using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System.Collections.Generic;
using System;
using API.Models;

namespace API.Controllers
{
    public class MongoCRUD
    {
        private IMongoDatabase db;

        public MongoCRUD(string database = "ADOPSE") 
        { 
            var client = new MongoClient("mongodb://localhost:27017/");
            db = client.GetDatabase(database); //"ADOPSE"
        }

         //Read methods
        public List<T> getList<T>() 
        {
            var collection = db.GetCollection<T>(ModelsCommon.TypeToTable<T>());
            return collection.Find(new BsonDocument()).ToList();
        }

        public T getRecord<T>(Guid id) {
            var collection = db.GetCollection<T>(ModelsCommon.TypeToTable<T>());
            return collection.Find(Builders<T>.Filter.Eq("ID", id)).First();
        }

        //insert method
        public void Insert<T>(T record)
        { 
            var collection = db.GetCollection<T>(ModelsCommon.TypeToTable<T>());
            collection.InsertOne(record);
        }

        //insert or update method
        public void Upsert<T>(Guid id, T record)
        {
            var collection = db.GetCollection<T>(ModelsCommon.TypeToTable<T>());
            var result = collection.ReplaceOne(
                new BsonDocument("_id", id),
                record,
                new ReplaceOptions { IsUpsert = true });
        }

        //delete method
        public void Delete<T>(Guid id) 
        { 
            var collection = db.GetCollection<T>(ModelsCommon.TypeToTable<T>());
            collection.DeleteOne(Builders<T>.Filter.Eq("ID", id));
        }
    }
}