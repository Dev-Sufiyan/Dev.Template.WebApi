using Dev.Template.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dev.Template.Repositories.Config;

public class SentItemsConfig : IEntityTypeConfiguration<SentItems>
{
    public void Configure(EntityTypeBuilder<SentItems> builder)
    {
        builder.HasKey(x => new { x.ProfileId, x.TimeStamp });
        builder.Property(x => x.TimeStamp).IsRequired();
        builder.Property(x => x.Count).IsRequired();

        // Configure foreign key relationship
        builder.HasOne(x => x.UserProfile)
               .WithMany()
               .HasForeignKey(x => x.ProfileId);
    }
}
