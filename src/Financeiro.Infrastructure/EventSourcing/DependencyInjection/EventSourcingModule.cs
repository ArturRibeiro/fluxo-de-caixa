namespace Financeiro.Infrastructure.EventSourcing.DependencyInjection;

public static class EventSourcingModule
{
    public static IServiceCollection AddEventSourcing(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<EventStoreDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<IEventStore, SqlEventStore>();
        services.AddSingleton<IEventSerializer, JsonEventSerializer>();
        services.AddSingleton<IEventTypeResolver, EventTypeResolver>();

        return services;
    }
}