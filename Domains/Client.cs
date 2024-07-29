using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MinimalAPIMongo.Domains
{
    public class Client
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("_userId"), BsonRepresentation(BsonType.ObjectId)]
        public string? UserId { get; set;}

        [BsonElement("cpf")]
        public string? Cpf { get; set;}

        [BsonElement("phone")]
        public string? Phone { get; set; }

        [BsonElement("address")]
        public string? Address { get; set; }

        public Dictionary<string, string> AddicionalAtributes { get; set; }

        public Client()
        {
            AddicionalAtributes = new Dictionary<string, string>();
        }
    }
}
