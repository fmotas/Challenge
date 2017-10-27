using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System.Collections.Generic;

namespace Challenge.Models
{
    public class DataAccess
    {
        MongoClient _client;
        MongoServer _server;
        MongoDatabase _db;

        public DataAccess()
        {
            _client = new MongoClient("mongodb://localhost:27017");
            _server = _client.GetServer();
            _db = _server.GetDatabase("ChallengeDB");
        }

        public IEnumerable<Item> GetItems() // GET ALL ITEMS
        {
            return _db.GetCollection<Item>("Items").FindAll();
        }


        public Item GetItem(ObjectId id) // GET AN ITEM BY ITS ID
        {
            var res = Query<Item>.EQ(i => i.Id, id);
            return _db.GetCollection<Item>("Items").FindOne(res);
        }
        public IEnumerable<Item> GetItem(char itemInUse) // GET ALL ITEMS WITH AN SPECIFIC "ItemInUse" SETTING
        {
            var res = Query<Item>.EQ(i => i.ItemInUse, itemInUse);
            return _db.GetCollection<Item>("Items").Find(res);
        }

        public Item Create(Item i) // CREATE AN ITEM
        {
            _db.GetCollection<Item>("Items").Save(i);
            return i;
        }

        public void Update(ObjectId id, Item i) // UPDATE AN SPECIFIC ITEM USING ITS ID AND A NEW ITEM PASSED BY THE CLIENT
        {
            i.Id = id;
            var res = Query<Item>.EQ(itm => itm.Id, id);
            var operation = Update<Item>.Replace(i);
            _db.GetCollection<Item>("Items").Update(res, operation);
        }
        public void Remove(ObjectId id) // REMOVE AN ITEM BY ITS ID
        {
            var res = Query<Item>.EQ(e => e.Id, id);
            var operation = _db.GetCollection<Item>("Items").Remove(res);
        }
    }
}
