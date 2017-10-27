using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Challenge.Models
{
    public class Item
    {
        public ObjectId Id { get; set; }

        [BsonElement("ItemId")]
        public int ItemId { get; set; }

        [BsonElement("ItemName")]
        public string ItemName { get; set; }

        [BsonElement("ItemInUse")]
        public char ItemInUse { get; set; }

        [BsonElement("ItemFloor")]
        public int ItemFloor { get; set; }
    }
}
