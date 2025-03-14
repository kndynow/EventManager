using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventManager.Core.Models;

namespace EventManager.Core.Validator
{
    public class EventValidator : IEventValidator
    {
        //Very simple eventvalidator, can add more logic later

        public bool CheckIfValidEvent(Event ev)
        {
            if (string.IsNullOrEmpty(ev.Name))
                return false;

            if (ev.MaxAttendees <= 0 || ev.MaxAttendees > 100000)
                return false;

            return true;
        }
    }
}
