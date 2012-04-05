using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace FunForce.LostCities.Presentation.Client
{
    public class TypeLoader
    {
        public static IList<Type> LoadTypes(string[] assemblyPaths, Type interfaceType)
        {
            return GetTypesFromAssemblyPaths(assemblyPaths, interfaceType);
        }

        private static IList<Type> GetTypesFromAssemblyPaths(string[] assemblyPaths, Type interfaceType)
        {
            IList<Type> types = new List<Type>();
            foreach (string path in assemblyPaths)
            {
                Assembly assembly = Assembly.LoadFile(path);
                System.Type typeFound = GetImplementedTypes(assembly, interfaceType);
                if (typeFound != null)
                    types.Add(typeFound);
                else
                    Console.WriteLine(string.Format("assembly {0} does not contain an implementation for {1}", path, interfaceType.FullName));
            }
            return types;
        }

        private static System.Type GetImplementedTypes(Assembly assembly, Type interfaceType)
        {
            foreach (Type type in assembly.GetTypes())
            {
                System.Type[] interfaces = type.GetInterfaces();
                foreach (System.Type iface in interfaces)
                {
                    if (iface.Equals(interfaceType))
                    {
                        return type;
                    }
                }
            }
            return null;
        }
    }
}
