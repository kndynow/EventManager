using EventManager.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventManager.Core.Models;
using EventManager.Core.Validator;

namespace EventManager.Core.Services
{
    public interface IEventService
    {
        Task<IEnumerable<Event>> GetAllEventsAsync();
        Task<string> CreateEventAsync(Event newEvent);
        Task<Event> GetEventByIdAsync(string id);
    }

    //Handles the business logic
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IEventValidator _eventValidator;

        public EventService(IEventRepository eventRepository, IEventValidator eventValidator)
        {
            _eventRepository = eventRepository;
            _eventValidator = eventValidator;
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            return await _eventRepository.GetAllAsync();
        }

        public async Task<string> CreateEventAsync(Event newEvent)
        {
            if (!_eventValidator.CheckIfValidEvent(newEvent))
            {
                throw new ArgumentException("The event data is not valid.");
            }

            await _eventRepository.CreateAsync(newEvent);
            return newEvent.Id;
        }

        public async Task<Event> GetEventByIdAsync(string id)
        {
            var ev = await _eventRepository.GetByIdAsync(id);

            if (ev == null)
            {
                throw new KeyNotFoundException("Event not found.");
            }

            return ev;
        }
    }
}
