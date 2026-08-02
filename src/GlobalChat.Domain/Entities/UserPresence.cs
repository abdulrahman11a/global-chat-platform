using GlobalChat.Domain.Enums;

namespace GlobalChatPlatform.Domain.Entities;

public class UserPresence
{
    public Guid UserId { get; set; }
    public PresenceStatus Status { get; set; }
    public DateTime LastSeenAt { get; set; }
}
