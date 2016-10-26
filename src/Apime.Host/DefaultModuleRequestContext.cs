using Microsoft.AspNetCore.Http;

namespace Apime.Host
{
    public class DefaultModuleRequestContext : BaseModuleRequestContext
    {
        public DefaultModuleRequestContext(HttpContext context) : base(context)
        {
        }
    }
}
