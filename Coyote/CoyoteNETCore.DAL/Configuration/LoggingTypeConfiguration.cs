using CoyoteNETCore.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoyoteNETCore.DAL.Configuration
{
    public class LoggingTypeConfiguration : IEntityTypeConfiguration<LoggingType>
    {
        public void Configure(EntityTypeBuilder<LoggingType> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
