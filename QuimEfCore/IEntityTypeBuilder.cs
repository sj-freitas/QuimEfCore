using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QuimEfCore
{
    public interface IEntityTypeBuilder<TEntity> where TEntity : class
    {
        void Configure(EntityTypeBuilder<TEntity> entityTypeBuilder);
    }
}
