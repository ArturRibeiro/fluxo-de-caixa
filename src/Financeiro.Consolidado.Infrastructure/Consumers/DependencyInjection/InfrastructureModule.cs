namespace Financeiro.Consolidado.Infrastructure.Consumers.DependencyInjection;

public static class MassTransitModule
{
    public static IServiceCollection AddMassTransitModule<T>(this IServiceCollection services,
        IConfiguration configuration, Action<IBusRegistrationContext, IReceiveEndpointConfigurator> action)
        where T : class
    {
        var host = configuration.GetSection("RabbitMQ:HostName").Value;
        var userName = configuration.GetSection("RabbitMQ:UserName").Value;
        var password = configuration.GetSection("RabbitMQ:Password").Value;
        
        services.AddMassTransit(x =>
        {
            x.AddConsumers(typeof(T).Assembly);

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(host, h =>
                {
                    h.Username(userName);
                    h.Password(password);
                });

                //cfg.ReceiveEndpoint("financeiro.movimentacao.lancada.queue", ep => ep.ConfigureConsumer<MovimentacaoLancadaConsumer>(context));
                cfg.ReceiveEndpoint("financeiro.movimentacao.lancada.queue", ep => action(context, ep));
            });
        });
        return services;
    }
}