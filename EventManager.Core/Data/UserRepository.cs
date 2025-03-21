using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventManager.Core.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace EventManager.Core.Data
{
    public interface IUserRepository
    {
        Task<User?> GetByUsernameAsync(string username);

        Task CreateAsync(User user);
    }

    public class UserRepository : IUserRepository
    {
        // Handles interactions with the MongoDB database for user-related data.
        // Connects to MongoDB server via MongoDbContext.
        private readonly IMongoCollection<User> _users;

        public UserRepository(MongoDbContext context)
        {
            _users = context.Users;
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
