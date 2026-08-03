using GlobalChat.Domain.Common;
using GlobalChat.Domain.Entities;
using GlobalChat.Domain.Enums;

namespace GlobalChat.Domain.Entities;

public class Message : BaseEntity
{
    public Guid SenderId { get; set; }
    public required string Content { get; set; }
    public MessageType Type { get; set; }
    public MessageStatus Status { get; set; }
    public DateTime? EditedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public Guid ChannelId { get; set; }
    public Channel Channel { get; set; } = null!;
    public ICollection<Attachment> Attachments { get; set; } = [];
}
