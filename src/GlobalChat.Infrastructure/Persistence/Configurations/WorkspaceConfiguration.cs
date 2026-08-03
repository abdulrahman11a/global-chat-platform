using GlobalChat.Domain.Entities;
using GlobalChat.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GlobalChat.Infrastructure.Persistence.Configurations;

public class WorkspaceConfiguration : IEntityTypeConfiguration<Workspace>
{
    public void Configure(EntityTypeBuilder<Workspace> builder)
    {
        builder.Property(w => w.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.HasOne<ApplicationUser>()
               .WithMany()
               .HasForeignKey(w => w.OwnerId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(w => w.Channels)
               .WithOne(c => c.Workspace)
               .HasForeignKey(c => c.WorkspaceId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(w => w.Members)
               .WithOne(m => m.Workspace)
               .HasForeignKey(m => m.WorkspaceId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
