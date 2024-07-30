using MinimalAPIMongo.Domains;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace MinimalAPIMongo.ViewModel
{
    public class OrderViewModel
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

        [BsonIgnore]
        [JsonIgnore]
        public virtual ICollection<Product>? Products { get; set; } = new List<Product>();

        [BsonElement("clientId")]
        public string? ClientId { get; set; }

        [BsonIgnore]
        [JsonIgnore]
        public virtual Client? Client { get; set; }
    }
}
