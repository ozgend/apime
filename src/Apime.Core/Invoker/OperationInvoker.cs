using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Apime.Core.Invoker
{
    public class OperationInvoker
    {

        public OperationInvoker()
        {
        }

        public static OperationInvoker Create()
        {
            return new OperationInvoker();
        }

        public async Task<object> Invoke(InvokerContext context)
        {
            return await Task.Factory.StartNew(() => InvokeTask(context));
        }

        private object InvokeTask(InvokerContext context)
        {
            var method = GetMethod(context);

            var arguments = ConvertToTypedMethodArguments(context, method);
            object result;

            try
            {
                result = method.Invoke(context.ObjectInstance, arguments);
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Cannot invoke method [{0}]", method.Name), ex);
            }

            var safeResult = EncapsulateReturnValue(result, method.Name);
            return safeResult;
        }

        private MethodInfo GetMethod(InvokerContext context)
        {
            MethodInfo targetMethod = null;
            try
            {
                targetMethod = context.ObjectInstance.GetType().GetMethod(context.Method);
            }
            catch (Exception)
            {
                var methods = context.ObjectInstance.GetType().GetMethods();

                foreach (var method in methods)
                {
                    if (targetMethod != null)
                    {
                        break;
                    }

                    // omitted uncompilable code: ie. parameterless duplicated methods
                    var parameters = method.GetParameters();
                    var parameterChecks = new bool[parameters.Length];
                    var index = 0;
                    foreach (var parameter in parameters)
                    {
                        var value = context.Parameter[parameter.Name];
                        parameterChecks[index++] = value != null;
                        if (value == null)
                        {
                            break;
                        }
                    }

                    if (parameterChecks.Any(p => p == false))
                    {
                        continue;
                    }

                    targetMethod = method;
                }

            }

            if (targetMethod == null)
            {
                throw new Exception(String.Format("Method [{0}] not found in Type <{1}>", context.Method, context.Type));
            }

            return targetMethod;
        }

        private object[] ConvertToTypedMethodArguments(InvokerContext context, MethodInfo method)
        {
            object[] arguments = { };
            var parameters = method.GetParameters();
            if (parameters.Length == 0)
            {
                return arguments;
            }
            arguments = parameters.Select(p => CreateMethodParameterInstance(p.ParameterType, context.Parameter, p.Name)).ToArray();
            return arguments;
        }

        private object CreateMethodParameterInstance(Type inputParameterType, dynamic contextParameter, string parameterName)
        {
            var contextObject = contextParameter[parameterName] as JToken;

            if (contextObject == null)
            {
                throw new Exception(string.Format("Json.Net cannot cast contextParameter[{0}] as JToken", parameterName));
            }

            try
            {
                var value = contextObject.ToObject(inputParameterType);
                return value;
            }
            catch (Exception ex)
            {

                throw new Exception(string.Format("Json.Net cannot convert '{0}' in Type <{1}> as JContainer", parameterName, inputParameterType.Name), ex);
            }
        }

        private object EncapsulateReturnValue(object value, string methodName)
        {
            var key = string.Format("{0}Result", methodName);
            return new Dictionary<string, object> { { key, value } };
        }

    }

}
