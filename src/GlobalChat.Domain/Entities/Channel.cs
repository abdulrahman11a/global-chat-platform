using GlobalChat.Domain.Common;
using GlobalChat.Domain.Entities;
using GlobalChat.Domain.Enums;
namespace GlobalChat.Domain.Entities;

public class Channel : BaseEntity
{
    public string? Name { get; set; }
    public ChannelType Type { get; set; }

    public Guid WorkspaceId { get; set; }
    public Workspace Workspace { get; set; } = null!;
    public ICollection<ChannelMember> Members { get; set; } = [];
    public ICollection<Message> Messages { get; set; } = [];
}
