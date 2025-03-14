using EventManager.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventManager.Core.Models;

namespace EventManager.Core.Services
{

    //Handles the business logic
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;

        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<string> CreateEventAsync(Event newEvent)
        {
            await _eventRepository.CreateAsync(newEvent);
            return newEvent.Id;
        }
    }
}
