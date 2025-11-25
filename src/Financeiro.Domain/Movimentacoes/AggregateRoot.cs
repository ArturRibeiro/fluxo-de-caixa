namespace Financeiro.Domain.Movimentacoes;

public abstract class AggregateRoot
{
    private readonly List<IDomainNotification> _events = new();

    public IReadOnlyCollection<IDomainNotification> Events => _events;

    protected void AddEvent(IDomainNotification domainEvent) 
        => _events.Add(domainEvent);

    public void ClearEvents() => _events.Clear();
}