namespace Financeiro.Infrastructure.EventSourcing.EventStore.Imp;

public class EventDataModel
{
    public long? Id { get; set; }
    public Guid AggregateId { get; set; }
    public string EventType { get; set; } = default!;
    public string EventData { get; set; } = default!;
    public DateTime OccurredOn { get; set; }  = DateTime.UtcNow;
    public bool IsPublished { get; set; } = false;
}