using System;
using System.IO;
using Apime.Core.Invoker;
using Newtonsoft.Json;

namespace Apime.CliTest
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var binPath = Path.Combine(AppContext.BaseDirectory, "plugins");

            const string serializedCalendarServiceCreateData = "{\"person\":{\"Firstname\":\"Den\",\"Lastname\":\"Olk\"},\"event\":{\"Date\":\"2016-03-24T00:00:53.1894199+02:00\",\"Title\":\"Sample event\"}}";
            const string serializedContactServiceAddData = "{\"person\":{\"Firstname\":\"Den\",\"Lastname\":\"Olk\"}}";
            const string serializedContactServiceAddListData = "{\"persons\":[{\"Firstname\":\"Den\",\"Lastname\":\"Olk\"}]}";
            const string serializedCalculatorServiceSumData = "{\"value1\":3,\"value2\":7}";
            const string serializedCalculatorServiceSumListData = "{\"values\":[3,-1,-2]}";

            var contextCalendarCreate = InvokerContext.Create(binPath, "SampleApi", "SampleApi.Services.CalendarService", "Create", serializedCalendarServiceCreateData);
            var contextCalendarGetAll = InvokerContext.Create(binPath, "SampleApi", "SampleApi.Services.CalendarService", "GetAll", null);
            var contextContactAdd = InvokerContext.Create(binPath, "SampleApi", "SampleApi.Services.ContactService", "Add", serializedContactServiceAddData);
            var contextContactAddList = InvokerContext.Create(binPath, "SampleApi", "SampleApi.Services.ContactService", "Add", serializedContactServiceAddListData);
            var contextCalculatorSum = InvokerContext.Create(binPath, "SampleApi", "SampleApi.Services.CalculatorService", "Sum", serializedCalculatorServiceSumData);
            var contextCalculatorSumList = InvokerContext.Create(binPath, "SampleApi", "SampleApi.Services.CalculatorService", "Sum", serializedCalculatorServiceSumListData);
            var contextExternalProcess = InvokerContext.Create(binPath, "SampleApi", "SampleApi.Services.ExternalService", "Send", serializedContactServiceAddData);

            var invoker = OperationInvoker.Create();

            var resultCalendarCreate = invoker.Invoke(contextCalendarCreate);
            var resultCalendarGetAll = invoker.Invoke(contextCalendarGetAll);
            var resultContactAdd = invoker.Invoke(contextContactAdd);
            var resultContactAddList = invoker.Invoke(contextContactAddList);
            var resultCalculatorSum = invoker.Invoke(contextCalculatorSum);
            var resultCalculatorSumList = invoker.Invoke(contextCalculatorSumList);
            var resultExternalProcess = invoker.Invoke(contextExternalProcess);

            Console.WriteLine("resultCalendarCreate = {0}\r\n", JsonConvert.SerializeObject(resultCalendarCreate));
            Console.WriteLine("resultCalendarGetAll = {0}\r\n", JsonConvert.SerializeObject(resultCalendarGetAll));
            Console.WriteLine("resultContactAdd = {0}\r\n", JsonConvert.SerializeObject(resultContactAdd));
            Console.WriteLine("resultContactAddList = {0}\r\n", JsonConvert.SerializeObject(resultContactAddList));
            Console.WriteLine("resultCalculatorSum = {0}\r\n", JsonConvert.SerializeObject(resultCalculatorSum));
            Console.WriteLine("resultCalculatorSumList = {0}\r\n", JsonConvert.SerializeObject(resultCalculatorSumList));
            Console.WriteLine("resultExternalProcess = {0}\r\n", JsonConvert.SerializeObject(resultExternalProcess));

            Console.ReadLine();

        }

    }
}
