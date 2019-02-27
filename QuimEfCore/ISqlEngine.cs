using Microsoft.EntityFrameworkCore;

namespace QuimEfCore
{
    public interface ISqlEngine
    {
        void AddSqlEngine(DbContextOptionsBuilder dbContextOptionsBuilder);
    }
}
