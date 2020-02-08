using CoyoteNET.Shared;
using CoyoteNET.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoyoteNET.DAL.Configuration
{
    public class ThreadEditConfiguration : IEntityTypeConfiguration<ThreadEdit>
    {
        public void Configure(EntityTypeBuilder<ThreadEdit> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Editor);
            builder.HasOne(x => x.Thread);
        }
    }
}
