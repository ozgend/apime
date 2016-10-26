using System.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using Apime.Sdk.Contracts;

namespace Apime.Core.Assembly
{
    internal class ApiAssemblyLoader : AssemblyLoadContext
    {
        private readonly string _probeDirectory;

        public ApiAssemblyLoader(string probeDirectory)
        {
            _probeDirectory = probeDirectory;
        }

        protected override System.Reflection.Assembly Load(AssemblyName assemblyName)
        {
            var path = Path.Combine(_probeDirectory, assemblyName.Name) + ".dll";
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("assembly not found", path);
            }

            return LoadFromAssemblyPath(path);
        }

        protected System.Reflection.Assembly Load(string assemblyName)
        {
            return Load(new AssemblyName(assemblyName));
        }


        public object LoadAndUnwrap(string assemblyName, string typeName)
        {
            var assemblyFiles = Directory.GetFiles(_probeDirectory, "*.dll");
            Type type = null;

            if (string.IsNullOrEmpty(assemblyName))
            {
                foreach (var itemAssemblyName in assemblyFiles.Select(Path.GetFileNameWithoutExtension))
                {
                    type = ExtractType(typeName, itemAssemblyName);
                    if (type != null)
                    {
                        break;
                    }
                }
            }
            else
            {
                if (assemblyFiles.Any(file => file.Contains(assemblyName)))
                {
                    type = ExtractType(typeName, assemblyName);
                }
            }

            if (type == null)
            {
                throw new Exception(string.Format("cannot find type <{0}> in [{1}]", typeName, string.Join(",", assemblyFiles)));
            }


            var instance = Activator.CreateInstance(type);

            if (instance == null)
            {
                throw new Exception(string.Format("cannot create instance of <{0}> in [{1}]", typeName, assemblyName));
            }

            return instance;
        }

        private Type ExtractType(string typeName, string assemblyName)
        {
            var assembly = Load(assemblyName);
            var types = assembly.GetTypes();
            var type = types.FirstOrDefault(t => t.FullName.Contains(typeName));
            return type;
        }
    }

}
