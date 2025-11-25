namespace Financeiro.Infrastructure.DependencyInjection;

public static class InfrastructureModule
{
    public static void AddInfrastructureModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEventSourcing(configuration);
        services.AddQuartzModule();
        services.AddMassTransitConfig(configuration);
        services.AddCommandApplicationModule();
        services.AddScoped<IMovimentacaoFinanceiraRepository, MovimentacaoFinanceiraRepository>();
        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>((sp, options) =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
            //.AddInterceptors(sp.GetRequiredService<AuditLogInterceptor>())
        );
    }
}