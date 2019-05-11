﻿using CoyoteNETCore.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoyoteNETCore.DAL.Configuration
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Author);
            builder.HasOne(x => x.Thread);
            builder.HasMany(x => x.Comments);
            builder.HasMany(x => x.Editions);
        }
    }
}
