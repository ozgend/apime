using System.Diagnostics;

namespace Apime.Sdk.Module
{
    public abstract class BaseApiModule
    {
        public void Initialize()
        {
            Debug.WriteLine("initialize base");
        }
    }
}
