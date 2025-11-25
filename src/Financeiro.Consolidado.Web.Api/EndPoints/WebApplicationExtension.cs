namespace Financeiro.Consolidado.Web.Api.EndPoints;

public static class WebApplicationExtension
{
    public static WebApplication MapEndpointMovimentacaoConsolidada
        (this WebApplication app)
    {
        app.MapGet("/api/consolidado", async (
                DateOnly data,
                IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                var query = new MovimentacaoConsolidadaQuery(data);
                var response = await mediator.Send(query, cancellationToken);
                return Results.Ok(response);
            })
            .Produces(StatusCodes.Status200OK, typeof(object))
            .Produces(StatusCodes.Status500InternalServerError)
            .WithOpenApi(MovimentacaoConsolidadaDocumentacao.CreateSummay);
        
        app.MapGet("/weatherforecast", (DateOnly data) =>
            {
                return data;
            })
            .WithName("GetWeatherForecast")
            .WithOpenApi();

        return app;
    }
}