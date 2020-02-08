using CoyoteNET.Shared;
using CoyoteNET.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoyoteNET.DAL.Configuration
{
    public class ForumSectionConfiguration : IEntityTypeConfiguration<ForumSection>
    {
        public void Configure(EntityTypeBuilder<ForumSection> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
