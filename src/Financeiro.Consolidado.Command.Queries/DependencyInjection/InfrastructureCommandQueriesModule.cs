using System.Diagnostics.CodeAnalysis;

namespace Financeiro.Consolidado.Command.Queries.DependencyInjection;

[ExcludeFromCodeCoverage]
public static class InfrastructureCommandQueriesModule
{
    public static IServiceCollection AddInfrastructureCommandQueriesModule(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        return services;
    }
}