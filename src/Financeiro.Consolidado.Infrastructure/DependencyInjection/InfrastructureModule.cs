using Financeiro.Consolidado.Command.Queries;
using Financeiro.Consolidado.Command.Queries.DependencyInjection;
using Financeiro.Consolidado.Commands.DependencyInjection;

namespace Financeiro.Consolidado.Infrastructure.DependencyInjection;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructureModule(this IServiceCollection services
        , IConfiguration configuration)
    {
        services.AddInfrastructureCommandModule();
        services.AddInfrastructureCommandQueriesModule();
        services.AddScoped<IPeriodoRepository, PeriodoRepository>();
        services.AddDbContext<IApplicationDbContextReading, ApplicationDbContextReading>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }
}