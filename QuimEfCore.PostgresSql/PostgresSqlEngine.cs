using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace QuimEfCore.PostgresSql
{
    public class PostgresSqlEngine : ActionSqlEngine
    {
        public static Action<DbContextOptionsBuilder> Get(string connectionString,
            Assembly migrationsAssembly = null)
        {
            return builder =>
                builder.UseNpgsql(
                    connectionString,
                    options =>
                    {
                        if (migrationsAssembly != null)
                        {
                            options.MigrationsAssembly(migrationsAssembly
                                .GetName()
                                .Name);
                        }
                    });
        }

        public PostgresSqlEngine(string connectionString,
            Assembly migrationsAssembly = null)
            : base(Get(connectionString, migrationsAssembly))
        {
        }
    }
}
