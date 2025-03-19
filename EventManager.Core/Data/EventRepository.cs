using EventManager.Core.Models;
using MongoDB.Driver;

namespace EventManager.Core.Data
{
    public interface IEventRepository
    {
        Task CreateAsync(Event newEvent);
        Task<Event> GetByIdAsync(string id);
        Task<IEnumerable<Event>> GetAllAsync();
        Task UpdateAsync(Event updatedEvent);
        Task UpdatePartialAsync(Event updatedEvent);
        Task DeleteAsync(string id);
    }

    public class EventRepository : IEventRepository
    {
        // Handles interactions with the MongoDB database for event-related data.
        // Connects to MongoDB server via MongoDbContext.
        private readonly IMongoCollection<Event> _events;

        // Constructor
        public EventRepository(MongoDbContext context)
        {
            _events = context.Events;
        }

        // Reusable filter to get event by id
        private static FilterDefinition<Event> GetIdFilter(string id) =>
            Builders<Event>.Filter.Eq(e => e.Id, id);

        //Create event
        public async Task CreateAsync(Event newEvent)
        {
            await _events.InsertOneAsync(newEvent);
        }

        //Fetches all events and returns it as a list of Event objects
        public async Task<IEnumerable<Event>> GetAllAsync()
        {
            var filter = Builders<Event>.Filter.Empty;
            return await _events.Find(filter).ToListAsync();
        }

        //Get event by id
        public async Task<Event> GetByIdAsync(string id)
        {
            var filter = GetIdFilter(id);
            return await _events.Find(filter).FirstOrDefaultAsync();
        }

        // Update event
        public async Task UpdateAsync(Event updatedEvent)
        {
            var filter = GetIdFilter(updatedEvent.Id);
            await _events.ReplaceOneAsync(filter, updatedEvent);
        }

        // Update event partially
        public async Task UpdatePartialAsync(Event updatedEvent)
        {
            var filter = GetIdFilter(updatedEvent.Id);
            var update = Builders<Event>
                .Update.Set(e => e.Name, updatedEvent.Name)
                .Set(e => e.Description, updatedEvent.Description)
                .Set(e => e.Type, updatedEvent.Type)
                .Set(e => e.StartTime, updatedEvent.StartTime)
                .Set(e => e.EndTime, updatedEvent.EndTime)
                .Set(e => e.MaxAttendees, updatedEvent.MaxAttendees);

            await _events.UpdateOneAsync(filter, update);
        }

        // Delete event
        public async Task DeleteAsync(string id)
        {
            var filter = GetIdFilter(id);
            var result = await _events.DeleteOneAsync(filter);

            if (result.DeletedCount == 0)
            {
                throw new KeyNotFoundException($"Event with id {id} not found.");
            }
        }
    }
}
