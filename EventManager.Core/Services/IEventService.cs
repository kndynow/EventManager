using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventManager.Core.Models;

namespace EventManager.Core.Services
{
    public interface IEventService
    {
        Task<string> CreateEventAsync(Event newEvent);
    }
}
