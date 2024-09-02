using Genie.Counter.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Genie.Counter.DBContext.Config;

public class SentItemsConfig : IEntityTypeConfiguration<SentItems>
{
    public void Configure(EntityTypeBuilder<SentItems> builder)
    {
        builder.HasKey(x => new { x.ProfileId, x.TimeStamp }); // Composite Key or use appropriate primary key configuration
        builder.Property(x => x.TimeStamp).IsRequired();
        builder.Property(x => x.Count).IsRequired();

        // Configure foreign key relationship
        builder.HasOne(x => x.UserProfile)
               .WithMany()
               .HasForeignKey(x => x.ProfileId);
    }
}
