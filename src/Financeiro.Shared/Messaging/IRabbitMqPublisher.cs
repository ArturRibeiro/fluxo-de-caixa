namespace Financeiro.Shared.Messaging;

public interface IMessagingPublisher
{
    Task PublishAsync<T>(T message) where T : class; 
    Task PublishAsync<T>(List<T> messages) where T : class;
}

