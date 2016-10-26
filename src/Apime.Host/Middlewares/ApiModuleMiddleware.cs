using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Apime.Core.Invoker;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Apime.Host.Middlewares
{
    public class ApiModuleMiddleware
    {

        private readonly RequestDelegate _next;

        public ApiModuleMiddleware(RequestDelegate next, System.IServiceProvider provider)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var moduleRequest = new DefaultModuleRequestContext(context);

            //for testing, below directory should contain SampleApi output assemblies within a folder named same with X-ApiKey request header
            var binPath = Path.Combine(AppContext.BaseDirectory, "plugins", moduleRequest.ApiKey);

            var invokerContext = InvokerContext.Create(binPath, moduleRequest.Class, moduleRequest.Method, moduleRequest.SerializedContent);
            var invoker = OperationInvoker.Create();

            var result = await invoker.Invoke(invokerContext);
            var serialized = JsonConvert.SerializeObject(result);
            var bytes = Encoding.UTF8.GetBytes(serialized);

            context.Response.ContentType = "application/json";

            await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
        }
    }
}
