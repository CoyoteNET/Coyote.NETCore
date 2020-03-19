﻿using CoyoteNET.Shared;
using CoyoteNET.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoyoteNET.DAL.Configuration
{
    public class PostCommentEditConfiguration : IEntityTypeConfiguration<PostCommentEdit>
    {
        public void Configure(EntityTypeBuilder<PostCommentEdit> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Editor);
            builder.HasOne(x => x.Comment);
        }
    }
}
