namespace Financeiro.Infrastructure.Messaging;

public class RabbitMqPublisher(IPublishEndpoint publishEndpoint, ILogger<RabbitMqPublisher> logger) : IMessagingPublisher
{
    public Task PublishAsync<T>(T message)
        where T : class 
        => publishEndpoint.Publish(message);

    public Task PublishAsync<T>(List<T> messages) where T : class
    {
        foreach (var message in messages)
        {
            try
            {
                publishEndpoint.Publish(message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Falha ao publicar evento {evt}", message.GetType().Name);
            }
        }
        return Task.CompletedTask;
    }
}
