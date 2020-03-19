using CoyoteNET.Shared;
using CoyoteNET.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoyoteNET.DAL.Configuration
{
    public class LoggingTypeConfiguration : IEntityTypeConfiguration<LoggingType>
    {
        public void Configure(EntityTypeBuilder<LoggingType> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
