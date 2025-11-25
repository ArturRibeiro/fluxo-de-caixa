using Financeiro.Infrastructure;
using Financeiro.Infrastructure.DependencyInjection;
using Financeiro.Infrastructure.EventSourcing.DependencyInjection;

namespace Financeiro.Web.Api;

public static class Startup 
{
    public static void Run
        (string[] args)
    {
        WebApplication
            .CreateBuilder(args)
            .ConfigureSerilogLog()
            .AddDependencies()
            .Build()
            .CreateDataBase()
            .MapEndpointLancaMovimentacao()
            .Configure()
            .Run();
    }

    static WebApplicationBuilder ConfigureSerilogLog(this WebApplicationBuilder builder)
    {
        // Log.Logger = new LoggerConfiguration()
        //     .WriteTo.File("logs/audit-.log", rollingInterval: RollingInterval.Day)
        //     .CreateLogger();
        //
        // builder.Host.UseSerilog();
        
        return builder;
    }

    static WebApplicationBuilder AddDependencies(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddInfrastructureModule(builder.Configuration);
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();
        return builder;
    }
    
    private static WebApplication CreateDataBase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
        context?.Seed(app.Configuration);
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