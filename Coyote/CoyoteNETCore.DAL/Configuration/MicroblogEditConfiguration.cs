using CoyoteNETCore.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoyoteNETCore.DAL.Configuration
{
    public class MicroblogEditConfiguration : IEntityTypeConfiguration<MicroblogEdit>
    {
        public void Configure(EntityTypeBuilder<MicroblogEdit> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Post);
            builder.HasOne(x => x.Editor);
        }
    }
}
