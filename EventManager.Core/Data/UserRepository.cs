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
        Task<User> GetByIdAsync(string id);
        Task<IEnumerable<User>> GetAllAsync();
        Task UpdatePartialAsync(User updatedUser);
        Task DeleteAsync(string id);
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

        // Reusable filter to get event by id
        private static FilterDefinition<User> GetIdFilter(string id) =>
            Builders<User>.Filter.Eq(u => u.Id, id);

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _users.Find(user => user.Username == username).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(User user)
        {
            await _users.InsertOneAsync(user);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var filter = Builders<User>.Filter.Empty;
            return await _users.Find(filter).ToListAsync();
        }

        public async Task<User> GetByIdAsync(string id)
        {
            var filter = GetIdFilter(id);
            return await _users.Find(filter).FirstOrDefaultAsync();
        }

        public async Task UpdatePartialAsync(User updatedUser)
        {
            var filter = GetIdFilter(updatedUser.Id);
            var update = Builders<User>.Update.Set(e => e.Role, updatedUser.Role);

            await _users.UpdateOneAsync(filter, update);
        }

        public async Task DeleteAsync(string id)
        {
            var filter = GetIdFilter(id);
            var result = await _users.DeleteOneAsync(filter);

            if (result.DeletedCount == 0)
            {
                throw new KeyNotFoundException($"Users with id {id} not found.");
            }
        }
    }
}
