using GlobalChat.Domain.Entities;
using GlobalChat.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GlobalChat.Infrastructure.Persistence.Configurations;

public class ChannelMemberConfiguration : IEntityTypeConfiguration<ChannelMember>
{
    public void Configure(EntityTypeBuilder<ChannelMember> builder)
    {
        builder.HasKey(cm => new { cm.ChannelId, cm.UserId });

        builder.HasOne<ApplicationUser>()
               .WithMany(u => u.ChannelMemberships)
               .HasForeignKey(cm => cm.UserId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
