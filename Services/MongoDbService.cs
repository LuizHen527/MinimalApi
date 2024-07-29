using MongoDB.Driver;

namespace MinimalAPIMongo.Services
{
    public class MongoDbService
    {
        /// <summary>
        /// Armazena a configuracao da aplicacao
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Armazena uma referencia ao mongoDB
        /// </summary>
        private readonly IMongoDatabase _database;

        public MongoDbService(IConfiguration configuration)
        {
            _configuration = configuration;

            var connectionString = _configuration.GetConnectionString("DbConnection");

            var mongoURL = MongoUrl.Create(connectionString);

            //Cria um client MongoClient para se conectar ao mongo
            var mongoClient = new MongoClient(mongoURL);

            _database = mongoClient.GetDatabase(mongoURL.DatabaseName);
        }

        /// <summary>
        /// propriedade para acessar o banco de dados
        /// </summary>
        public IMongoDatabase GetDatabase => _database;
    }
}
