using GlobalChat.Domain.Common;
using GlobalChat.Domain.Entities;
using GlobalChatPlatform.Domain.Entities;

namespace GlobalChat.Domain.Entities;

public class Workspace : BaseEntity
{
    public required string Name { get; set; }
    public Guid OwnerId { get; set; }

    public Guid SenderId { get; set; }
    public ICollection<WorkspaceMember> Members { get; set; } = [];
    public ICollection<Channel> Channels { get; set; } = [];
}
