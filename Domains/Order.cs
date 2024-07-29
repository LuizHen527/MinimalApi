using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MinimalAPIMongo.Domains
{
    public class Order
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("date")]
        public string? Date { get; set; }

        [BsonElement("status")]
        public string? Status { get; set; }
    }
}
