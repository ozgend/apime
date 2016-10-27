using System;
using System.Reflection;
using Apime.Core.Invoker;

namespace Apime.Core.Assembly
{
    internal static class ReflectionHelper
    {
        public static object CreateComponentInstance(InvokerContext context)
        {
            var assemblyResolverHelper = new ApiAssemblyLoader(context.BinPath);
            var instance = assemblyResolverHelper.LoadAndUnwrap(context.Assembly, context.Type);
            return instance;
        }

        public static MethodInfo GetMethod(object @object, string methodName)
        {
            Type type = @object.GetType();
            var method = type.GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance);
            return method;
        }

        public static TInstance CreateInstance<TInstance>()
        {
            return CreateInstance<TInstance>(null);
        }

        public static TInstance CreateInstance<TInstance>(object[] args)
        {
            var instance = CreateInstance(typeof(TInstance), args);
            return (TInstance)instance;
        }

        public static object CreateInstance(Type type, object[] args)
        {
            return args == null ? Activator.CreateInstance(type) : Activator.CreateInstance(type, args);
        }

        public static void SetPropertyValue(this object @object, string propertyName, object value)
        {
            @object.GetType().GetProperty(propertyName).SetValue(@object, value, null);
        }

    }
}
