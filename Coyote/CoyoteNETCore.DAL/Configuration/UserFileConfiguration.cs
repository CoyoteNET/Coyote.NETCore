using CoyoteNETCore.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoyoteNETCore.DAL.Configuration
{
    public class UserFileConfiguration : IEntityTypeConfiguration<UserFile>
    {
        public void Configure(EntityTypeBuilder<UserFile> builder)
        {

            builder
                .HasKey(bc => new { bc.UserId, bc.FileId});

            builder
                .HasOne(bc => bc.File)
                .WithMany(b => b.DownloadedBy)
                .HasForeignKey(bc => bc.FileId);

            //builder
            //    .HasOne(bc => bc.User)
            //    .WithMany(c => c.DownloadedFilesLog)
            //    .HasForeignKey(bc => bc.UserId);
        }
    }
}
