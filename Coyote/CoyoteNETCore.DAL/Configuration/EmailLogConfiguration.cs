using CoyoteNETCore.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoyoteNETCore.DAL.Configuration
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
