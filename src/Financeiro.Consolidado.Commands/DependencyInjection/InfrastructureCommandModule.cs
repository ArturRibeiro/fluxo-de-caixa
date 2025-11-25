using System.Diagnostics.CodeAnalysis;

namespace Financeiro.Consolidado.Commands.DependencyInjection;

[ExcludeFromCodeCoverage]
public static class InfrastructureCommandModule
{
    public static void AddInfrastructureCommandModule(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    }
}