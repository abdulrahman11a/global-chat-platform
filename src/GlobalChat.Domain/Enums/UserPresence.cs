
namespace GlobalChat.Domain.Enums;

public class UserPresence
{
    public Guid UserId { get; set; }
    public PresenceStatus Status { get; set; }
    public DateTime LastSeenAt { get; set; }

    public Guid SenderId { get; set; }
}
