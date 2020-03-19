using CoyoteNET.Shared;
using CoyoteNET.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoyoteNET.DAL.Configuration
{
    public class MicroblogCommentConfiguration : IEntityTypeConfiguration<MicroblogComment>
    {
        public void Configure(EntityTypeBuilder<MicroblogComment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Author);
            builder.HasOne(x => x.MicroblogEntry);
        }
    }
}
