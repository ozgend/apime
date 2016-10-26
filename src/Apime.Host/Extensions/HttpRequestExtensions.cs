using System;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Apime.Host.Extensions
{
    public static class HttpRequestExtensions
    {
        public static string BodyAsString(this HttpRequest request)
        {
            string content;
            using (var stream = request.Body)
            {
                using (var readStream = new StreamReader(stream, Encoding.UTF8))
                {
                    content = readStream.ReadToEnd();
                }
            }
            return content;
        }

        public static string[] Parts(this HttpRequest request)
        {
            return request.Path.Value.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string GetApiSecret(this HttpRequest request)
        {
            return request.GetHeader("X-ApiSecret");
        }

        public static string GetApiKey(this HttpRequest request)
        {
            return request.GetHeader("X-ApiKey");
        }

        public static string GetHeader(this HttpRequest request, string key)
        {
            return request.Headers[key].FirstOrDefault();
        }

        public static bool HasApiUser(this HttpRequest request)
        {
            return request.Headers.ContainsKey("X-ApiKey");
        }
    }
}
