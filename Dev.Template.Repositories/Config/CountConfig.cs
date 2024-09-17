using Dev.Template.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dev.Template.Repositories.Config;

public class CountConfig : IEntityTypeConfiguration<Count>
{
    public required DbSet<Count> Counts { get; set; }
    public void Configure(EntityTypeBuilder<Count> builder)
    {
        builder.HasKey(x => x.CounterId);
    }
}

