using CoyoteNETCore.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoyoteNETCore.DAL.Configuration
{
    public class ThreadConfiguration : IEntityTypeConfiguration<Thread>
    {
        public void Configure(EntityTypeBuilder<Thread> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Author);
            builder.HasOne(x => x.Category);
            builder.HasMany(x => x.ThreadEdits);
            builder.HasMany(x => x.Posts);
            builder.HasMany(x => x.Subscribers);
        }
    }
}
