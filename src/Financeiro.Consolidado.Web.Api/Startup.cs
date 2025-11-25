namespace Financeiro.Consolidado.Web.Api;

public static class Startup 
{
    public static void Run
        (string[] args)
    {
        WebApplication
            .CreateBuilder(args)
            .ConfigureAddMassTransit()
            .AddDependencies()
            .Build()
            .CreateDataBase()
            .MapEndpointMovimentacaoConsolidada()
            .Configure()
            .Run();
    }

    static WebApplicationBuilder ConfigureAddMassTransit(this WebApplicationBuilder builder)
    {
        builder.Services.AddMassTransitModule<Program>(builder.Configuration, (context, ep) => { ep.ConfigureConsumer<MovimentacaoLancadaConsumer>(context); });
        return builder;
    }

    static WebApplicationBuilder AddDependencies(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddInfrastructureModule(builder.Configuration);
        builder.Services.AddProblemDetails();
        return builder;
    }
    
    private static WebApplication CreateDataBase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var applicationDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        applicationDbContext.Seed(app.Configuration);
        return app;
    }

    static WebApplication Configure(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseHttpsRedirection();
        app.UseExceptionHandler();
        return app;
    }
}