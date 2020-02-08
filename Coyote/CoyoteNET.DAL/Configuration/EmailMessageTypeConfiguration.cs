using CoyoteNET.Shared;
using CoyoteNET.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoyoteNET.DAL.Configuration
{
    public class EmailMessageTypeConfiguration : IEntityTypeConfiguration<EmailMessageType>
    {
        public void Configure(EntityTypeBuilder<EmailMessageType> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
