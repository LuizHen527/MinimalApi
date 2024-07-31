using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace MinimalAPIMongo.Domains
{
    public class Order
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("date")]
        public DateTime? Date { get; set; }

        [BsonElement("status")]
        public string? Status { get; set; }

        [BsonElement("productId")]
        [JsonIgnore]
        public List<string>? ProductId { get; set; }

        public virtual List<Product>? Products { get; set; } = new List<Product>();

        [BsonElement("clientId")]
        [JsonIgnore]
        public string? ClientId { get; set; }

        public virtual Client? Client { get; set; }
    }
}
