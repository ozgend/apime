using System;

namespace SampleDependency
{
    public class ExternalOperations
    {
        public ExternalType Process(object value)
        {
            return new ExternalType { Data = value, Guid = Guid.NewGuid() };
        }
    }
}
