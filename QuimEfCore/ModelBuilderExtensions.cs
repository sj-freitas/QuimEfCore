using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace QuimEfCore
{
    public static class ModelBuilderExtensions
    {
        /// <summary>
        /// Runs all types in the specific assembly that implement IEntityTypeBuilder.
        /// </summary>
        /// <param name="modelBuilder">The model builder to configure.</param>
        /// <param name="assembly">The assembly to read the types from.</param>
        /// <param name="activator">An activator function that given a type
        /// creates an instance. This allows for a dependency injector to be
        /// used. If none is used, the Activator.CreateInstance is used.
        /// </param>
        public static void ConfigureFromAssembly(this ModelBuilder modelBuilder,
            Assembly assembly, Func<Type, object> activator = null)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            activator = activator ?? ((t) => Activator.CreateInstance(t));

            var allTypes = assembly
                .GetTypes()
                .Where(t => GetImplementingInterfaceOfEntityTypeBuilde(t) != null);

            var instances = allTypes
                .Select(activator);

            foreach (var curr in instances)
            {
                var type = GetImplementingInterfaceOfEntityTypeBuilde(curr.GetType());
                var entityType = type
                    .GetGenericArguments()
                    .First();

                var method = typeof(ModelBuilderExtensions)
                    .GetMethod(nameof(ConfigureEntity))
                    .MakeGenericMethod(entityType);

                method.Invoke(null, new[] { modelBuilder, curr });
            }
        }

        public static void ConfigureEntity<T>(this ModelBuilder modelBuilder,
            IEntityTypeBuilder<T> typeBuilder)
            where T : class
        {
            if (typeBuilder == null)
            {
                throw new ArgumentNullException(nameof(typeBuilder));
            }

            var entityTypeBuilder = modelBuilder.Entity<T>();

            typeBuilder.Configure(entityTypeBuilder);
        }

        private static Type GetImplementingInterfaceOfEntityTypeBuilde(Type type)
        {
            return type
                .GetInterfaces()
                .Where(interfaceType =>
                    interfaceType.IsGenericType &&
                    interfaceType.GetGenericTypeDefinition() == typeof(IEntityTypeBuilder<>)
                )
                .FirstOrDefault();
        }
    }
}
