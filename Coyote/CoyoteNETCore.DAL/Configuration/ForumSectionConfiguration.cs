using CoyoteNETCore.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

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
