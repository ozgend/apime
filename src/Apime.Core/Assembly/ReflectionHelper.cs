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

        //public static TAttribute GetPropertyAttribute<TAttribute>(this object item) where TAttribute : Attribute
        //{
        //    var member = item.GetType().GetMember(item.ToString());
        //    var attributes = member[0].GetCustomAttributes(typeof(TAttribute), false);
        //    return (attributes.Length > 0) ? (TAttribute)attributes[0] : null;
        //}

        //public static Type CreateType(string typeName, Dictionary<string, Type> propertyDefinition)
        //{
        //    var assemblyName = new AssemblyName
        //    {
        //        Name = "mbus.temporary.assembly"
        //    };

        //    var assemblyBuilder = Thread.GetDomain().DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
        //    var moduleBuilder = assemblyBuilder.DefineDynamicModule("mbus.temporary.module");
        //    var typeBuilder = moduleBuilder.DefineType(typeName, TypeAttributes.Public | TypeAttributes.Class);

        //    foreach (var pair in propertyDefinition)
        //    {
        //        var propertyName = pair.Key;
        //        var propertyType = pair.Value;

        //        var fieldBuilder = typeBuilder.DefineField("_" + propertyName, typeof(string), FieldAttributes.Private);
        //        var propertyBuilder = typeBuilder.DefineProperty(propertyName, PropertyAttributes.None, propertyType, new Type[] { propertyType });

        //        var methodAttributes = MethodAttributes.Public | MethodAttributes.HideBySig;
        //        var getterMethodBuilder = typeBuilder.DefineMethod("get_value", methodAttributes, propertyType, Type.EmptyTypes);

        //        var getterGenerator = getterMethodBuilder.GetILGenerator();
        //        getterGenerator.Emit(OpCodes.Ldarg_0);
        //        getterGenerator.Emit(OpCodes.Ldfld, fieldBuilder);
        //        getterGenerator.Emit(OpCodes.Ret);

        //        var setterMethodBuilder = typeBuilder.DefineMethod("set_value", methodAttributes, null, new Type[] { propertyType });

        //        var setterGenerator = setterMethodBuilder.GetILGenerator();
        //        setterGenerator.Emit(OpCodes.Ldarg_0);
        //        setterGenerator.Emit(OpCodes.Ldarg_1);
        //        setterGenerator.Emit(OpCodes.Stfld, fieldBuilder);
        //        setterGenerator.Emit(OpCodes.Ret);

        //        propertyBuilder.SetGetMethod(getterMethodBuilder);
        //        propertyBuilder.SetSetMethod(setterMethodBuilder);
        //    }

        //    Type generetedType = typeBuilder.CreateType();

        //    return generetedType;
        //}
    }
}
