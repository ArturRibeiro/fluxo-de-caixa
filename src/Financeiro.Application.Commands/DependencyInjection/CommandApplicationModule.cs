namespace Financeiro.Application.Commands.DependencyInjection;

[ExcludeFromCodeCoverage]
public static class CommandApplicationModule
{
    public static void AddCommandApplicationModule(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            //cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));
            //cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(CorrelationBehavior<,>));
        });
    }
}