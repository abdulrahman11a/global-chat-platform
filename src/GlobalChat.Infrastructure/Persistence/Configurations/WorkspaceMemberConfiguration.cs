using GlobalChat.Domain.Entities;
using GlobalChat.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GlobalChat.Infrastructure.Persistence.Configurations;

public class WorkspaceMemberConfiguration : IEntityTypeConfiguration<WorkspaceMember>
{
    public void Configure(EntityTypeBuilder<WorkspaceMember> builder)
    {
        builder.HasKey(wm => new { wm.WorkspaceId, wm.UserId });

        builder.HasOne<ApplicationUser>()
               .WithMany(u => u.WorkspaceMemberships)
               .HasForeignKey(wm => wm.UserId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
