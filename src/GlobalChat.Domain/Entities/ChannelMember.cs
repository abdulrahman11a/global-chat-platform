namespace GlobalChat.Domain.Entities;

public class ChannelMember
{
    public DateTime JoinedAt { get; set; }
    public Guid UserId { get; set; }

    public Guid ChannelId { get; set; }
    public Channel Channel { get; set; } = null!;
}
