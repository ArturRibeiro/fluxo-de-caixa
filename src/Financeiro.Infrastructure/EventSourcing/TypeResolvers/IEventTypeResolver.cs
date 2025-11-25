namespace Financeiro.Infrastructure.EventSourcing.TypeResolvers;

public interface IEventTypeResolver
{
    Type Resolve(string eventType);
    string GetEventTypeName(Type type);
}