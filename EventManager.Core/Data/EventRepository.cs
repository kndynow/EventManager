using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventManager.Core.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

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
        // Connects to MongoDB server via MongoDbContext.
        private readonly IMongoCollection<Event> _events;

        public EventRepository(MongoDbContext context)
        {
            _events = context.Events;
        }

        //Fetches all events and returns it as a list of Event objects
        public async Task<IEnumerable<Event>> GetAllAsync()
        {
            return await _events.Find(_ => true).ToListAsync();
        }

        //Create event
        public async Task CreateAsync(Event newEvent)
        {
            await _events.InsertOneAsync(newEvent);
        }

        //Get event by id
        public async Task<Event> GetByIdAsync(string id)
        {
            return await _events.Find(e => e.Id == id).FirstOrDefaultAsync();
        }
    }
}
