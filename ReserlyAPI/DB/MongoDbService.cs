using MongoDB.Driver;

namespace ReserlyAPI.DB
{
    public class MongoDbService
    {
        private readonly IMongoDatabase _database;

        public MongoDbService(IConfiguration configuration)
        {
            try
            {
                var client = new MongoClient(configuration.GetConnectionString("Connection"));
                _database = client.GetDatabase("ReservationSystemDB");
            }
            catch (MongoException ex)
            {
                throw new Exception("Could not connect to the MongoDB server.", ex);
            }
        }

        public IMongoDatabase Database => _database;
    }
}
    