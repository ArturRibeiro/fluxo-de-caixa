namespace Financeiro.Infrastructure.EventSourcing.Serializers.Imp;

public class JsonEventSerializer : IEventSerializer
{
    private static readonly JsonSerializerOptions Options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = false
    };

    public string Serialize(IDomainNotification @event)
    {
        return JsonSerializer.Serialize(@event, @event.GetType(), Options);
    }

    public IDomainNotification Deserialize(string data, Type eventType)
    {
        var result = JsonSerializer.Deserialize(data, eventType, Options);
        return (IDomainNotification)result!;
    }
}