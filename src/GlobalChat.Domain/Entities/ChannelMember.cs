namespace GlobalChat.Domain.Entities;

public class ChannelMember
{
    public Guid ChannelId { get; set; }
    public Guid UserId { get; set; }
    public DateTime JoinedAt { get; set; }

    public Channel Channel { get; set; } = null!;
    public Guid SenderId { get; set; }
}
