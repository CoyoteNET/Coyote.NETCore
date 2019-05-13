using CoyoteNETCore.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoyoteNETCore.DAL.Configuration
{
    public class MicroblogCommenEditConfiguration : IEntityTypeConfiguration<MicroblogCommentEdit>
    {
        public void Configure(EntityTypeBuilder<MicroblogCommentEdit> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Editor);
            builder.HasOne(x => x.Comment);
        }
    }
}
