using Microsoft.EntityFrameworkCore;
using System;

namespace QuimEfCore
{
    public static class DbContextOptionsBuilderExtensions
    {
        public static DbContextOptionsBuilder Use(
            this DbContextOptionsBuilder dbContextOptionsBuilder, ISqlEngine engine)
        {
            if (dbContextOptionsBuilder == null)
            {
                throw new ArgumentNullException(nameof(dbContextOptionsBuilder));
            }

            engine.AddSqlEngine(dbContextOptionsBuilder);

            return dbContextOptionsBuilder;
        }
    }
}
