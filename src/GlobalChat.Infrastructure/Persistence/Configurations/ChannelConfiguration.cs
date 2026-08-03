using GlobalChat.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GlobalChat.Infrastructure.Persistence.Configurations;

public class ChannelConfiguration : IEntityTypeConfiguration<Channel>
{
    public void Configure(EntityTypeBuilder<Channel> builder)
    {
        builder.Property(c => c.Name)
               .HasMaxLength(100);

        builder.HasMany(c => c.Members)
               .WithOne(m => m.Channel)
               .HasForeignKey(m => m.ChannelId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.Messages)
               .WithOne(m => m.Channel)
               .HasForeignKey(m => m.ChannelId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(c => new { c.WorkspaceId, c.Type });
    }
}
