using CoyoteNETCore.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoyoteNETCore.DAL.Configuration
{
    public class EmailMessageTypeConfiguration : IEntityTypeConfiguration<EmailMessageType>
    {
        public void Configure(EntityTypeBuilder<EmailMessageType> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
