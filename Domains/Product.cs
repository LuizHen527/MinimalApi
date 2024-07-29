using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MinimalAPIMongo.Domains
{
    public class Product
    {
        [BsonId]
        //define o nome do campo no MongoDb como "_id" e o tipo como "ObjectId"
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("name")]
        public string? name { get; set; }
        [BsonElement("price")]
        public decimal price { get; set; }

        //adiciona um dicionario para atributos adicionais
        public Dictionary<string, string> AdditionalAttributes { get; set; }

        /// <summary>
        /// ao ser instanciado um obj da classe Product, o atributo AddicionalAttributes ja vira com um novo dicionario e portanto
        /// habilitado 
        /// </summary>
        public Product() 
        {
        AdditionalAttributes = new Dictionary<string, string>();
        }
    }
}
