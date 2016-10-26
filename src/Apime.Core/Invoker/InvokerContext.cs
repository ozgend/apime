using Apime.Core.Assembly;
using Newtonsoft.Json;

namespace Apime.Core.Invoker
{
    public class InvokerContext
    {
        public string BinPath { get; set; }
        public string Assembly { get; set; }
        public string Type { get; set; }
        public string Method { get; set; }
        public dynamic Parameter { get; set; }
        public object ObjectInstance { get; set; }

        public static InvokerContext Create(string binPath, string assembly, string @class, string method, string serialized = null)
        {
            var context = new InvokerContext
            {
                BinPath = binPath,
                Assembly = assembly,
                Type = @class,
                Method = method,
                Parameter = serialized != null ? JsonConvert.DeserializeObject<dynamic>(serialized) : null
            };

            context.ObjectInstance = ReflectionHelper.CreateComponentInstance(context);
            return context;
        }

        public static InvokerContext Create(string binPath, string @class, string method, string serialized = null)
        {
            var context = new InvokerContext
            {
                BinPath = binPath,
                Type = @class,
                Method = method,
                Parameter = serialized != null ? JsonConvert.DeserializeObject<dynamic>(serialized) : null
            };

            context.ObjectInstance = ReflectionHelper.CreateComponentInstance(context);
            return context;
        }

    }
}
