using CoyoteNET.Shared;
using CoyoteNET.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoyoteNET.DAL.Configuration
{
    public class PostCommentConfiguration : IEntityTypeConfiguration<PostComment>
    {
        public void Configure(EntityTypeBuilder<PostComment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Author);
            builder.HasOne(x => x.Post);
        }
    }
}
