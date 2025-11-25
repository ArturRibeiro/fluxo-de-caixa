namespace Financeiro.Infrastructure.EventSourcing.Serializers;

public interface IEventSerializer
{
    string Serialize(IDomainNotification @event);
    IDomainNotification Deserialize(string data, Type eventType);
}