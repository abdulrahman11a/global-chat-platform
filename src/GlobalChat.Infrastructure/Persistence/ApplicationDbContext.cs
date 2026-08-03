using GlobalChat.Domain.Common;
using GlobalChat.Domain.Entities;
using GlobalChat.Infrastructure.Identity;
using GlobalChatPlatform.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GlobalChat.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>(options)
{
    #region DbSets
    /// <summary>
    /// Expression-bodied property.
    /// This is a shorthand way of defining a property that only has a getter. 
    /// </summary>
    public DbSet<Workspace> Workspaces => Set<Workspace>();
    public DbSet<WorkspaceMember> WorkspaceMembers => Set<WorkspaceMember>();
    public DbSet<Channel> Channels => Set<Channel>();
    public DbSet<ChannelMember> ChannelMembers => Set<ChannelMember>();
    public DbSet<Message> Messages => Set<Message>();
    public DbSet<Attachment> Attachments => Set<Attachment>();
    public DbSet<UserPresence> UserPresences => Set<UserPresence>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    #endregion

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Picks up every IEntityTypeConfiguration<T> in this assembly
        // (Configurations/*.cs) instead of registering them one by one.
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var utcNow = DateTimeOffset.UtcNow;

        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = utcNow;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = utcNow;
            }
        }

        // TODO once MediatR is wired up: collect entry.Entity.DomainEvents
        // here, publish them, then call entry.Entity.ClearDomainEvents()
        // — right after SaveChanges commits, so events only fire once
        // the transaction actually succeeds.

        return await base.SaveChangesAsync(cancellationToken);

        #region explanation 
        /// <summary>
        /// Overrides EF Core's <see cref="DbContext.SaveChangesAsync(CancellationToken)"/>
        /// to execute application-wide persistence logic before changes are committed.
        ///
        /// In this project (Vertical Slice Architecture), the <see cref="DbContext"/>
        /// acts as the Unit of Work, so common behaviors such as setting audit fields
        /// (e.g. CreatedAt and UpdatedAt) are centralized here instead of being repeated
        /// in every feature handler.
        ///
        /// In a traditional Clean Architecture, this logic is often placed inside a
        /// dedicated Unit of Work implementation. When Domain Events are introduced,
        /// they will be collected and published (typically via MediatR) after the
        /// database transaction completes successfully, ensuring events are dispatched
        /// only after the data has been persisted.
        /// </summary>
        /// <param name="cancellationToken">
        /// A token used to cancel the save operation.
        /// </param>
        /// <returns>
        /// The number of state entries written to the database.
        /// </returns>
        #endregion
    }
}
