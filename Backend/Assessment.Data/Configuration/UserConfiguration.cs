using Assessment.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assessment.Data.Configuration;

public class UserConfiguration : BaseConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .Property(x => x.Username)
            .HasMaxLength(255)
            .IsRequired();

        builder
            .HasMany<Call>(x => x.Calls)
            .WithOne(x => x.CallingUser);

        builder
            .HasData(new List<User>
            {
                new()
                {
                    Id = Guid.Parse("43baf7ac-2ba3-46f8-acf4-10b0795f34d1"),
                    Username = "Fin Coder",
                },
            });
        
        base.Configure(builder);
    }
}