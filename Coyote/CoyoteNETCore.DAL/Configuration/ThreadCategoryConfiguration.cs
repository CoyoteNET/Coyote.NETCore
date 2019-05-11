﻿using CoyoteNETCore.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoyoteNETCore.DAL.Configuration
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
