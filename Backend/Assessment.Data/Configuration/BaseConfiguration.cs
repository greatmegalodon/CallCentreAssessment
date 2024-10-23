using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Assessment.Data.Extensions;

namespace Assessment.Data.Configuration;

public class BaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : class
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        var interfaces = (builder
                .GetType()
                .GetGenericArguments()
                .FirstOrDefault()?
                .GetInterfaces() ?? Enumerable.Empty<Type>())
            .ToList();
		
        builder
            .ConfigureSoftDelete(interfaces)
            .ConfigureId(interfaces);
    }
}