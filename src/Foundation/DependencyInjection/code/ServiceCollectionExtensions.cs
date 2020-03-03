//    Copyright 2019 EPAM Systems, Inc.
// 
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//      http://www.apache.org/licenses/LICENSE-2.0
// 
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.

namespace Wooli.Foundation.DependencyInjection
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        private const string DefaultControllerFilter = "*Controller";

        public static void AddClassesWithServiceAttribute(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddClassesWithServiceAttribute(Assembly.GetCallingAssembly());
        }

        public static void AddClassesWithServiceAttribute(this IServiceCollection serviceCollection,
            params string[] assemblyFilters)
        {
            var assemblies = GetAssemblies(assemblyFilters);
            serviceCollection.AddClassesWithServiceAttribute(assemblies);
        }

        public static void AddClassesWithServiceAttribute(this IServiceCollection serviceCollection,
            params Assembly[] assemblies)
        {
            var typesWithAttributes = assemblies
                .Where(assembly => !assembly.IsDynamic)
                .SelectMany(GetExportedTypes)
                .Where(type => !type.IsAbstract && !type.IsGenericTypeDefinition)
                .Select(type =>
                    new
                    {
                        type.GetCustomAttribute<ServiceAttribute>()?.Lifetime,
                        ServiceType = type,
                        ImplementationType = type.GetCustomAttribute<ServiceAttribute>()?.ServiceType
                    })
                .Where(t => t.Lifetime != null);

            foreach (var type in typesWithAttributes)
                if (type.ImplementationType == null)
                    serviceCollection.Add(type.ServiceType, type.Lifetime.Value);
                else
                    serviceCollection.Add(type.ImplementationType, type.ServiceType, type.Lifetime.Value);
        }

        public static void AddByWildcard(this IServiceCollection serviceCollection, Lifetime lifetime,
            string classFilter, params Assembly[] assemblies)
        {
            if (assemblies == null || !assemblies.Any()) assemblies = new[] {Assembly.GetCallingAssembly()};

            var types = GetTypesImplementing(typeof(object), assemblies, classFilter);

            serviceCollection.Add(lifetime, types.ToArray());
        }

        public static void Add(this IServiceCollection serviceCollection, Lifetime lifetime, params Type[] types)
        {
            foreach (var type in types) serviceCollection.Add(type, lifetime);
        }

        public static void Add<T>(this IServiceCollection serviceCollection, Lifetime lifetime)
        {
            serviceCollection.Add(typeof(T), lifetime);
        }

        public static void Add(this IServiceCollection serviceCollection, Type type, Lifetime lifetime)
        {
            switch (lifetime)
            {
                case Lifetime.Singleton:
                    serviceCollection.AddSingleton(type);
                    break;
                case Lifetime.Transient:
                    serviceCollection.AddTransient(type);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, null);
            }
        }

        public static void Add(this IServiceCollection serviceCollection, Type serviceType, Type implementationType,
            Lifetime lifetime)
        {
            switch (lifetime)
            {
                case Lifetime.Singleton:
                    serviceCollection.AddSingleton(serviceType, implementationType);
                    break;
                case Lifetime.Transient:
                    serviceCollection.AddTransient(serviceType, implementationType);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, null);
            }
        }

        public static void AddTypesImplementingInCurrentAssembly<T>(this IServiceCollection serviceCollection,
            Lifetime lifeTime)
        {
            var types = GetTypesImplementing(typeof(T), Assembly.GetCallingAssembly());
            serviceCollection.Add(lifeTime, types.ToArray());
        }

        public static void AddTypesImplementing<T>(this IServiceCollection serviceCollection, Lifetime lifeTime,
            params string[] assemblies)
        {
            serviceCollection.AddTypesImplementing<T>(lifeTime, GetAssemblies(assemblies));
        }

        public static void AddTypesImplementing<T>(this IServiceCollection serviceCollection, Lifetime lifeTime,
            params Assembly[] assemblies)
        {
            var types = GetTypesImplementing(typeof(T), assemblies);
            serviceCollection.Add(lifeTime, types.ToArray());
        }

        public static void AddControllersInCurrentAssembly<T>(this IServiceCollection serviceCollection,
            params string[] classFilters)
        {
            AddControllers<T>(serviceCollection, Assembly.GetCallingAssembly());
        }

        public static void AddControllers<T>(this IServiceCollection serviceCollection, params string[] assemblyFilters)
        {
            serviceCollection.AddControllers<T>(GetAssemblies(assemblyFilters));
        }

        public static void AddControllers<T>(this IServiceCollection serviceCollection, params Assembly[] assemblies)
        {
            serviceCollection.AddControllers<T>(assemblies, new[] {DefaultControllerFilter});
        }

        public static void AddControllers<T>(this IServiceCollection serviceCollection, string[] assemblyFilters,
            params string[] classFilters)
        {
            serviceCollection.AddControllers<T>(GetAssemblies(assemblyFilters), classFilters);
        }

        private static void AddControllers<T>(this IServiceCollection serviceCollection,
            IEnumerable<Assembly> assemblies, string[] classFilters)
        {
            var controllers = GetTypesImplementing(typeof(T), assemblies, classFilters);

            foreach (var controller in controllers) serviceCollection.Add(controller, Lifetime.Transient);
        }

        private static Assembly[] GetAssemblies(IEnumerable<string> assemblyFilters)
        {
            var assemblies = new List<Assembly>();
            foreach (var assemblyFilter in assemblyFilters)
                assemblies.AddRange(AppDomain.CurrentDomain.GetAssemblies()
                    .Where(assembly => IsWildcardMatch(assembly.GetName().Name, assemblyFilter)).ToArray());

            return assemblies.ToArray();
        }

        private static IEnumerable<Type> GetTypesImplementing(Type implementsType, IEnumerable<Assembly> assemblies,
            params string[] classFilter)
        {
            var types = GetTypesImplementing(implementsType, assemblies.ToArray());
            if (classFilter != null && classFilter.Any())
                types = types.Where(type => classFilter.Any(filter => IsWildcardMatch(type.FullName, filter)));

            return types;
        }

        private static IEnumerable<Type> GetTypesImplementing(Type implementsType, params Assembly[] assemblies)
        {
            if (assemblies == null || assemblies.Length == 0) return new Type[0];

            var targetType = implementsType;

            return assemblies
                .Where(assembly => !assembly.IsDynamic)
                .SelectMany(GetExportedTypes)
                .Where(type => !type.IsAbstract && !type.IsGenericTypeDefinition && targetType.IsAssignableFrom(type))
                .ToArray();
        }

        private static IEnumerable<Type> GetExportedTypes(Assembly assembly)
        {
            try
            {
                return assembly.GetExportedTypes();
            }
            catch (NotSupportedException)
            {
                // A type load exception would typically happen on an Anonymously Hosted DynamicMethods
                // Assembly and it would be safe to skip this exception.
                return Type.EmptyTypes;
            }
            catch (FileLoadException)
            {
                // The assembly points to a not found assembly - ignore and continue
                return Type.EmptyTypes;
            }
            catch (ReflectionTypeLoadException ex)
            {
                // Return the types that could be loaded. Types can contain null values.
                return ex.Types.Where(type => type != null);
            }
            catch (Exception ex)
            {
                // Throw a more descriptive message containing the name of the assembly.
                throw new InvalidOperationException(
                    string.Format(CultureInfo.InvariantCulture, "Unable to load types from assembly {0}. {1}",
                        assembly.FullName, ex.Message), ex);
            }
        }

        /// <summary>
        ///     Checks if a string matches a wildcard argument (using regex)
        /// </summary>
        private static bool IsWildcardMatch(string input, string wildcard)
        {
            return input == wildcard || Regex.IsMatch(input,
                       "^" + Regex.Escape(wildcard).Replace("\\*", ".*").Replace("\\?", ".") + "$",
                       RegexOptions.IgnoreCase);
        }
    }
}