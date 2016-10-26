using System;
using System.Net.Http;
using Apime.Host.Extensions;
using Microsoft.AspNetCore.Http;

namespace Apime.Host
{
    public abstract class BaseModuleRequestContext
    {
        public string Class { get; private set; }
        public string Method { get; private set; }
        public string SerializedContent { get; private set; }
        
        public string ApiKey { get; private set; }
        public string ApiSecret { get; private set; }
        public HttpContext HttpContext { get; private set; }

        protected BaseModuleRequestContext(HttpContext context)
        {
            Create(context);
        }

        public  void Create(HttpContext context)
        {
            if (!context.Request.Path.HasValue)
            {
                throw new HttpRequestException("invalid request");
            }

            var hasApiUser = context.Request.HasApiUser();

            if (!hasApiUser)
            {
                throw new UnauthorizedAccessException("apikey and/or apisecret is not found");
            }

            var parts = context.Request.Parts();
            Class = parts[0];
            Method = parts[1];
            HttpContext = context;
            SerializedContent = context.Request.BodyAsString();
            ApiKey = context.Request.GetApiKey();
            //ApiSecret = context.Request.GetApiSecret();
            
        }

    }
}