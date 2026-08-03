using GlobalChat.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GlobalChat.Infrastructure.Persistence.Configurations;

public class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
{
    public void Configure(EntityTypeBuilder<Attachment> builder)
    {

        builder.Property(a => a.FileName).IsRequired().HasMaxLength(255);
        builder.Property(a => a.Url).IsRequired().HasMaxLength(2048);
        builder.Property(a => a.ContentType).IsRequired().HasMaxLength(100);
        builder.HasQueryFilter(a => a.Message.DeletedAt == null);
    }
}
