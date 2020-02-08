using CoyoteNET.Shared;
using CoyoteNET.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoyoteNET.DAL.Configuration
{
    public class ThreadCategoryConfiguration : IEntityTypeConfiguration<ThreadCategory>
    {
        public void Configure(EntityTypeBuilder<ThreadCategory> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Section);
        }
    }
}
