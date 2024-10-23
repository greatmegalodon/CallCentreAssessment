using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Assessment.Data.Entities;

namespace Assessment.Data.Configuration;

public class CallConfiguration : BaseConfiguration<Call>
{
    public override void Configure(EntityTypeBuilder<Call> builder)
    {
        builder
            .Property(x => x.DateCallStarted)
            .IsRequired();

        builder
            .HasOne<User>(x => x.CallingUser)
            .WithMany(x => x.Calls)
            .HasForeignKey(x => x.CallingUserId)
            .IsRequired();

        builder
            .HasData(new List<Call>
            {
                new()
                {
                    Id = Guid.Parse("f6fd57fa-c0fb-4c2d-b94c-2e7de08c0e89"),
                    CallingUserId = Guid.Parse("43baf7ac-2ba3-46f8-acf4-10b0795f34d1"),
                    DateCallStarted = DateTimeOffset.Now,
                },
            });
        
        base.Configure(builder);
    }
}