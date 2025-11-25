namespace Financeiro.Infrastructure.Messaging.DependencyInjection;

public static class MassTransitConfigModule
{
    public static void AddMassTransitConfig(this IServiceCollection services, IConfiguration configuration)
    {
        var host = configuration.GetSection("RabbitMQ:HostName").Value;
        var userName = configuration.GetSection("RabbitMQ:UserName").Value;
        var password = configuration.GetSection("RabbitMQ:Password").Value;
        
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((ctx, bus) =>
            {
                bus.Host(host, h =>
                {
                    h.Username(userName);
                    h.Password(password);
                });
                bus.ConfigureEndpoints(ctx);
            });
        });

        services.AddScoped<IMessagingPublisher, RabbitMqPublisher>();
    }
}