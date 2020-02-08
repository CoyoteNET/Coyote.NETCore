﻿using CoyoteNET.Shared;
using CoyoteNET.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoyoteNET.DAL.Configuration
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Author);
            builder.HasMany(x => x.Comments);
            builder.HasMany(x => x.Editions);

            builder.HasMany(x => x.Comments).WithOne(x => x.Post).HasForeignKey(x => x.PostId);
        }
    }
}
