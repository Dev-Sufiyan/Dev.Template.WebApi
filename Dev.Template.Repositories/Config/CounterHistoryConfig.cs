using Dev.Template.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dev.Template.Repositories.Config;

public class CountHistoryConfig : IEntityTypeConfiguration<CountHistory>
{
    public required DbSet<CountHistory> Counts { get; set; }
    public void Configure(EntityTypeBuilder<CountHistory> builder)
    {
        builder.HasKey(x => x.Id);
    }
}

