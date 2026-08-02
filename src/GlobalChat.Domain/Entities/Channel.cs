using GlobalChat.Domain.Common;
using GlobalChat.Domain.Entities;
using GlobalChat.Domain.Enums;
namespace GlobalChat.Domain.Entities;

public class Channel : BaseEntity
{
    public Guid WorkspaceId { get; set; }
    public string? Name { get; set; }
    public ChannelType Type { get; set; }

    public Workspace Workspace { get; set; } = null!;
    public ICollection<ChannelMember> Members { get; set; } = [];
    public ICollection<Message> Messages { get; set; } = [];
}
