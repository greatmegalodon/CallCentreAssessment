using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Assessment.Data.Interfaces;

namespace Assessment.Data.Extensions;

public static class EntityTypeBuilderExtensions
{
    public static EntityTypeBuilder<TEntity> ConfigureSoftDelete<TEntity>(
        this EntityTypeBuilder<TEntity> builder,
        IEnumerable<Type> baseTypes)
        where TEntity : class
    {
        if (!baseTypes.Contains(typeof(ISoftDelete)))
            return builder;
            
        builder.HasQueryFilter(x => !((ISoftDelete)x).DateDeleted.HasValue);

        return builder;
    }
    
    public static EntityTypeBuilder<TEntity> ConfigureId<TEntity>(
        this EntityTypeBuilder<TEntity> builder,
        IEnumerable<Type> baseTypes)
        where TEntity : class
    {
        builder.HasKey("Id");
        builder.Property("Id")
            .HasDefaultValueSql("NEWSEQUENTIALID()");

        return builder;
    }
}