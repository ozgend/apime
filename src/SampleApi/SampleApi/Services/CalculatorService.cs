using System.Collections.Generic;
using System.Linq;
using Apime.Sdk.Contracts;

namespace SampleApi.Services
{
    public class CalculatorService : IApiService
    {
        public int Sum(int value1, int value2)
        {
            return value1 + value2;
        }

        public int Sum(List<int> values)
        {
            return values.Sum();
        }
    }
}
