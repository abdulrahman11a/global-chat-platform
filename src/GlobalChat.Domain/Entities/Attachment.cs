using GlobalChat.Domain.Common;
using GlobalChat.Domain.Enums;
namespace GlobalChat.Domain.Entities;

public class Attachment : BaseEntity
{
    public Guid MessageId { get; set; }
    public required string FileName { get; set; }
    public required string Url { get; set; }
    public long Size { get; set; }
    public required string ContentType { get; set; }
    public AttachmentType Type { get; set; }

    public Message Message { get; set; } = null!;
}
