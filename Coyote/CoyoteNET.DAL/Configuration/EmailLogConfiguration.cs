using CoyoteNET.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoyoteNET.DAL.Configuration
{
    public class EmailLogConfiguration : IEntityTypeConfiguration<EmailLog>
    {
        public void Configure(EntityTypeBuilder<EmailLog> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Type);
        }
    }
}
