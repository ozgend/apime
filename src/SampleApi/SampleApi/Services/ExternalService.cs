using SampleApi.Models;
using SampleDependency;

namespace SampleApi.Services
{
    public class ExternalService
    {
        public ExternalType Send(Person person)
        {
            var external = new ExternalOperations();
            var result = external.Process(person);
            return result;
        }
    }
}
