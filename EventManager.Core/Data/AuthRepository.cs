using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventManager.Core.Models;



namespace EventManager.Core.Data
{
    public interface IAuthRepository
    {
        Task<User?> GetByUsernameAsync(string username);

        Task CreateAsync(User user);
    }
    public class AuthRepository : IAuthRepository
    {
        // Handles interactions with the MongoDB database for user-related data.
        // It connects to the MongoDB server using the connection string and database name from the MongoDbSettings-class.

        private readonly IMongoCollection<User> _users;

        public AuthRepository(IOptions<MongoDbSettings> options)
        {
            var settings = options.Value;
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _users = database.GetCollection<User>("Users");
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _users.Find(user => user.Username == username).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(User user)
        {
            await _users.InsertOneAsync(user);
        }


    }
}
