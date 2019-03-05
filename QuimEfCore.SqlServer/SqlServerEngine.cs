using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace QuimEfCore.SqlServer
{
    public class SqlServerEngine : ActionSqlEngine
    {
        public static Action<DbContextOptionsBuilder> Get(string connectionString,
            Assembly migrationsAssembly = null)
        {
            return builder =>
                builder.UseSqlServer(
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

        public SqlServerEngine(string connectionString,
            Assembly migrationsAssembly = null) 
            : base(Get(connectionString, migrationsAssembly))
        {
        }
    }
}
