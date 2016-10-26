using System;
using System.Collections.Generic;
using SampleApi.Models;
using System.Linq;
using Apime.Sdk.Contracts;

namespace SampleApi.Services
{
    public class ContactService : IApiService
    {
        public string Add(Person person)
        {
            return Guid.NewGuid().ToString();
        }

        public List<string> Add(List<Person> persons)
        {
            return persons.Select(Add).ToList();
        }
    }
}
