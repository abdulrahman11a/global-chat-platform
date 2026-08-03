using GlobalChat.Infrastructure.Identity;
using GlobalChatPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GlobalChat.Infrastructure.Persistence.Configurations;
public class UserPresenceConfiguration : IEntityTypeConfiguration<UserPresence>
{
    public void Configure(EntityTypeBuilder<UserPresence> builder)
    {
        builder.HasKey(p => p.UserId);

        builder.HasOne<ApplicationUser>()
               .WithOne(u => u.Presence)
               .HasForeignKey<UserPresence>(p => p.UserId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
