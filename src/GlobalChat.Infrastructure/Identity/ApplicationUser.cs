using GlobalChat.Domain.Entities;
using GlobalChat.Domain.Enums;
using GlobalChatPlatform.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace GlobalChat.Infrastructure.Identity;

public sealed class ApplicationUser : IdentityUser<Guid>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? ProfilePictureUrl { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    // Presence (IsOnline / LastSeen) intentionally NOT here —
    // lives in UserPresence instead, since it changes far more
    // often than the rest of this row.

    public ICollection<RefreshToken> RefreshTokens { get; set; } = [];
    public ICollection<WorkspaceMember> WorkspaceMemberships { get; set; } = [];
    public ICollection<ChannelMember> ChannelMemberships { get; set; } = [];
    public ICollection<Message> Messages { get; set; } = [];
    public UserPresence? Presence { get; set; }
}
