using System.Collections.Generic;
using System.Threading.Tasks;
using EventManager.Client.Models;

namespace EventManager.Client.Services
{
    public interface IEventService
    {
        Task<List<Event>> GetEventsAsync();
        Task<Event> GetEventByIdAsync(int id);
        Task<Event> CreateEventAsync(Event evt);
        Task<Event> UpdateEventAsync(Event evt);
        Task DeleteEventAsync(int id);
        Task<List<Event>> SearchEventsAsync(string searchTerm);
    }
} 