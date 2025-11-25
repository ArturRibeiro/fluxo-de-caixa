namespace Financeiro.Infrastructure.EventSourcing.EventStore.Imp;

public class SqlEventStore : IEventStore
{
    private readonly EventStoreDbContext _dbContext;
    private readonly IEventSerializer _serializer;
    private readonly IEventTypeResolver _typeResolver;

    public SqlEventStore(
        EventStoreDbContext dbContext,
        IEventSerializer serializer,
        IEventTypeResolver typeResolver)
    {
        _dbContext = dbContext;
        _serializer = serializer;
        _typeResolver = typeResolver;
    }

    public async Task AppendAsync(
        Guid aggregateId,
        IDomainNotification domainEvent,
        CancellationToken cancellationToken = default)
    {
        var eventData = new EventDataModel
        {
            AggregateId = aggregateId,
            EventType = _typeResolver.GetEventTypeName(domainEvent.GetType()),
            EventData = _serializer.Serialize(domainEvent)
        };

        _dbContext.Events.Add(eventData);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}