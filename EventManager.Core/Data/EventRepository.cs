using MongoDB.Driver;
using EventManager.Core.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace EventManager.Core.Data
{
    public interface IEventRepository
    {
        Task CreateAsync(Event newEvent);
        Task<Event> GetByIdAsync(string id);
        Task<IEnumerable<Event>> GetAllAsync();
    }

    public class EventRepository : IEventRepository
    {
        // Handles interactions with the MongoDB database for event-related data.
        // It connects to the MongoDB server using the connection string and database name from the MongoDbSettings-class.

        private readonly IMongoCollection<Event> _events;

        public EventRepository(IOptions<MongoDbSettings> options)
        {
            var settings = options.Value;
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _events = database.GetCollection<Event>("Events");
        }

        //Fetches all events and returns it as a list of Event objects
        public async Task<IEnumerable<Event>> GetAllAsync()
        {
            return await _events.Find(_ => true).ToListAsync();
        }

        public async Task CreateAsync(Event newEvent)
        {
            await _events.InsertOneAsync(newEvent);
        }

        public async Task<Event> GetByIdAsync(string id)
        {
            return await _events.Find(e => e.Id == id).FirstOrDefaultAsync();
        }
    }
}