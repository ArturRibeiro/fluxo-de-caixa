using Financeiro.Shared.DomainEvents;

namespace Financeiro.Shared;

public interface IEventStore
{
    Task AppendAsync(
        Guid aggregateId,
        IDomainNotification domainEvent,
        CancellationToken cancellationToken = default);
}