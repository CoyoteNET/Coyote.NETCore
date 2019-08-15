using CoyoteNETCore.Shared;
using CoyoteNETCore.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoyoteNETCore.DAL.Configuration
{
    public class ForumSectionConfiguration : IEntityTypeConfiguration<ForumSection>
    {
        public void Configure(EntityTypeBuilder<ForumSection> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
