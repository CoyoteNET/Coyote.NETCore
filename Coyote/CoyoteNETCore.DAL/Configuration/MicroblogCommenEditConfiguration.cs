using CoyoteNETCore.Shared;
using CoyoteNETCore.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoyoteNETCore.DAL.Configuration
{
    public class MicroblogCommenEditConfiguration : IEntityTypeConfiguration<MicroblogCommentEdit>
    {
        public void Configure(EntityTypeBuilder<MicroblogCommentEdit> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Editor);
            builder.HasOne(x => x.Comment);
        }
    }
}
