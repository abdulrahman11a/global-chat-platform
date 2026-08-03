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
    #region Soft Delete

    /// <summary>
    /// Soft delete timestamp.
    /// 
    /// We intentionally use DateTime? instead of a bool IsDeleted
    /// so we know not only whether the message was deleted,
    /// but also exactly when it happened.
    /// </summary>
    public DateTime? DeletedAt { get; set; }

    #endregion

    public Guid ChannelId { get; set; }
    public Channel Channel { get; set; } = null!;
    public ICollection<Attachment> Attachments { get; set; } = [];
}
