using EventManager.Core.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace EventManager.Core
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        // Setting up centralized connection to the MongoDB database
        public MongoDbContext(IConfiguration configuration)
        {
            var connectionString = configuration["MongoDbSettings:ConnectionString"];
            var databaseName = configuration["MongoDbSettings:DatabaseName"];
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        //Exposing collections to other classes
        public IMongoCollection<Event> Events => _database.GetCollection<Event>("Events");
        public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
        public IMongoCollection<Booking> Bookings => _database.GetCollection<Booking>("Bookings");
    }
}
