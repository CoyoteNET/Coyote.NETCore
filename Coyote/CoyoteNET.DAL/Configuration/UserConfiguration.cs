﻿using CoyoteNET.Shared;
using CoyoteNET.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoyoteNET.DAL.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .HasOne(x => x.Avatar)
                .WithOne(x => x.UploadedBy)
                .HasForeignKey<File>(x => x.UploadedById);

            builder.HasOne(x => x.Role);
            builder.HasMany(x => x.LoggingInAttempts);
            builder.HasMany(x => x.Posts);
            builder.HasMany(x => x.Notifications);

            builder
                .HasMany(x => x.DownloadedFilesLog)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);

            builder
                .HasMany(x => x.LoggingInAttempts)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);
        }
    }
}
