using EventManager.Core.Data;
using EventManager.Core.Models;
using EventManager.Core.Validator;

namespace EventManager.Core.Services
{
    public interface IEventService
    {
        Task<Event> CreateEventAsync(Event newEvent);
        Task<IEnumerable<Event>> GetAllEventsAsync();
        Task<Event> GetEventByIdAsync(string id);
        Task<Event> UpdateEventAsync(string id, Event updatedEvent);
        Task<Event> UpdateEventPartialAsync(string id, Event updatedEvent);
        Task<Event> DeleteEventAsync(string id);
    }

    //Handles the business logic
    public class EventService : IEventService
    {
        //Dependency injection
        private readonly IEventRepository _eventRepository;
        private readonly IEventValidator _eventValidator;

        //Constructor
        public EventService(IEventRepository eventRepository, IEventValidator eventValidator)
        {
            _eventRepository = eventRepository;
            _eventValidator = eventValidator;
        }

        //Creates a new event
        public async Task<Event> CreateEventAsync(Event newEvent)
        {
            if (!_eventValidator.CheckIfValidEvent(newEvent))
            {
                throw new ArgumentException("The event data is not valid.");
            }

            await _eventRepository.CreateAsync(newEvent);
            return newEvent;
        }

        //Fetches all events
        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            return await _eventRepository.GetAllAsync();
        }

        //Fetches an event by id
        public async Task<Event> GetEventByIdAsync(string id)
        {
            var ev = await _eventRepository.GetByIdAsync(id);
            //If event is not found, throw an exception
            if (ev == null)
            {
                throw new KeyNotFoundException("Event not found.");
            }

            return ev;
        }

        //Updates an event
        public async Task<Event> UpdateEventAsync(string id, Event updatedEvent)
        {
            var ev = await _eventRepository.GetByIdAsync(id);
            if (ev == null)
            {
                throw new KeyNotFoundException("Event not found.");
            }

            await _eventRepository.UpdateAsync(updatedEvent);
            return updatedEvent;
        }

        //Updates an event partially
        public async Task<Event> UpdateEventPartialAsync(string id, Event updatedEvent)
        {
            var ev = await _eventRepository.GetByIdAsync(id);
            if (ev == null)
            {
                throw new KeyNotFoundException("Event not found.");
            }

            await _eventRepository.UpdatePartialAsync(updatedEvent);
            return updatedEvent;
        }

        //Deletes an event
        public Task<Event> DeleteEventAsync(string id)
        {
            var ev = GetEventByIdAsync(id);
            if (ev is null)
            {
                throw new KeyNotFoundException("Event not found.");
            }

            _eventRepository.DeleteAsync(id);
            return ev;
        }
    }
}
