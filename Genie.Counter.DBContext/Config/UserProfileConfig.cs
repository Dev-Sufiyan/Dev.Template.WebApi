using Genie.Counter.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Genie.Counter.DBContext.Config;

public class UserProfileConfig : IEntityTypeConfiguration<UserProfile>
{
    public void Configure(EntityTypeBuilder<UserProfile> builder)
    {
        builder.HasKey(x => x.UserId);
        builder.Property(x => x.Name).HasMaxLength(100);
        builder.HasIndex(x => x.MobileNo).IsUnique();
    }
}
    

