using Genie.Counter.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Genie.Counter.DBContext.Config;

public class CountConfig : IEntityTypeConfiguration<Count>
{
    public required DbSet<Count> Counts { get; set; }
    public void Configure(EntityTypeBuilder<Count> builder)
    {
        builder.HasKey(x => x.Id);
    }
}

