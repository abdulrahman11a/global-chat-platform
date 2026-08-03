using GlobalChat.Domain.Entities;
using GlobalChat.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GlobalChat.Infrastructure.Persistence.Configurations;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.Property(m => m.Content)
               .IsRequired()
               .HasMaxLength(4000);

        builder.HasOne<ApplicationUser>()
               .WithMany(u => u.Messages)
               .HasForeignKey(m => m.SenderId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(m => m.Attachments)
               .WithOne(a => a.Message)
               .HasForeignKey(a => a.MessageId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(m => new { m.ChannelId, m.CreatedAt });

        // Soft-delete: messages with DeletedAt set are excluded by default.
        builder.HasQueryFilter(m => m.DeletedAt == null);
    }
}

