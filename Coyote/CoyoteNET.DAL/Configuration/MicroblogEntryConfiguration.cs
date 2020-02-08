using CoyoteNET.Shared;
using CoyoteNET.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoyoteNET.DAL.Configuration
{
    public class MicroblogEntryConfiguration : IEntityTypeConfiguration<MicroblogEntry>
    {
        public void Configure(EntityTypeBuilder<MicroblogEntry> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Author);
            builder.HasMany(x => x.Editions);
        }
    }
}
