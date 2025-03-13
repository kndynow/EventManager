using MongoDB.Driver;
using EventManager.Core.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Core.Data
{
    public class EventRepository
    {
        private readonly IMongoCollection<Event> _events;

        public EventRepository(IOptions<MongoDbSettings> options)
        {
            var settings = options.Value;
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _events = database.GetCollection<Event>("Events");
        }

        public async Task CreateAsync(Event newEvent)
        {
            await _events.InsertOneAsync(newEvent);
        }
    }
}
