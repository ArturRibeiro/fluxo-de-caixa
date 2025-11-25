namespace Financeiro.Infrastructure.EventSourcing.TypeResolvers.Imp;

public class EventTypeResolver : IEventTypeResolver
{
    private readonly Dictionary<string, Type> _cache = new();

    public Type Resolve(string eventType)
    {
        if (_cache.TryGetValue(eventType, out var type))
            return type;

        // Procura em todos os assemblies carregados por um tipo com esse nome
        type = AppDomain.CurrentDomain
                   .GetAssemblies()
                   .SelectMany(a => a.GetTypes())
                   .FirstOrDefault(t => t.Name == eventType)
               ?? throw new InvalidOperationException($"Tipo de evento não encontrado: {eventType}");

        _cache[eventType] = type;
        return type;
    }

    public string GetEventTypeName(Type type) => type.Name;
}