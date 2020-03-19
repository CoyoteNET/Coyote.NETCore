using CoyoteNET.Shared;
using CoyoteNET.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoyoteNET.DAL.Configuration
{
    public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder
                .HasDiscriminator<string>("SubscriptionType")
                .HasValue<SubscriptionMicroblogEntry>("Microblog")
                .HasValue<SubscriptionPost>("Post")
                .HasValue<SubscriptionThread>("Thread");
        }
    }
}
