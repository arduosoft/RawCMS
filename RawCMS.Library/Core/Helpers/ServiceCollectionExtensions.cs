using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace RawCMS.Library.Core.Helpers
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterAllTypes<T>(this IServiceCollection services, Assembly assembly,
        ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            var t = typeof(T);

            Type[] types = assembly.GetTypes();
            foreach (Type type in types)
            {
                try
                {
                    if ((t.IsAssignableFrom(type) || (type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == t)))
                        && !type.IsAbstract && !type.IsInterface && !type.IsGenericType)
                    {
                        services.Add(new ServiceDescriptor(typeof(T), type, lifetime));
                    }
                }
                catch
                {
                    // TODO: logging
                }
            }
        }
        
        public static void RegisterAllTypes<T>(this IServiceCollection services, Assembly[] assemblies,
        ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            var t = typeof(T);
            foreach (Assembly assembly in assemblies)
            {
                RegisterAllTypes<T>(services, assembly, lifetime);
            }   
        }
    }
}
