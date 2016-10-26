using System;
using System.Collections.Generic;
using Apime.Sdk.Contracts;
using SampleApi.Models;

namespace SampleApi.Services
{
    public class CalendarService : IApiService
    {
        public CalendarItem Create(Person person, Event @event)
        {
            return new CalendarItem
            {
                Id = Guid.NewGuid().ToString(),
                Person = person,
                Event = @event
            };
        }

        public List<CalendarItem> GetAll()
        {
            return new List<CalendarItem>
            {
                new CalendarItem
                {
                    Id = Guid.NewGuid().ToString(),
                    Event = new Event
                    {
                        Date = DateTime.Now,
                        Title = "Sample event"
                    },
                    Person = new Person
                    {
                        Firstname = "Den",
                        Lastname = "Olk"
                    }
                }
            };
        }
    }
}
