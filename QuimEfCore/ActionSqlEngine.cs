using Microsoft.EntityFrameworkCore;
using System;

namespace QuimEfCore
{
    public class ActionSqlEngine : ISqlEngine
    {
        private readonly Action<DbContextOptionsBuilder> _addSqlEngine;

        public ActionSqlEngine(Action<DbContextOptionsBuilder> addSqlEngine)
        {
            _addSqlEngine = addSqlEngine;
        }

        public void AddSqlEngine(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            _addSqlEngine(dbContextOptionsBuilder);
        }
    }
}
