namespace GlobalChat.Domain.Common;

public interface IDomainEvent
{
    DateTimeOffset OccurredAt { get; }
}
