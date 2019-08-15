using CoyoteNETCore.Shared;
using CoyoteNETCore.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoyoteNETCore.DAL.Configuration
{
    public class LoggingEntryConfiguration : IEntityTypeConfiguration<LoggingEntry>
    {
        public void Configure(EntityTypeBuilder<LoggingEntry> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Type);
            builder.HasOne(x => x.User);
        }
    }
}
