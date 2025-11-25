namespace Financeiro.Web.Api.EndPoints;

public static class WebApplicationExtension
{
    public static WebApplication MapEndpointLancaMovimentacao
        (this WebApplication app)
    {
        app.MapPost("/api/movimentacao", async (
                [FromBody] LancaMovimentacaoFinanceiraCommand command,
                IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                await mediator.Send(command, cancellationToken);
                return Results.Created();
            })
            .Produces(StatusCodes.Status201Created, typeof(object))
            .ProducesValidationProblem(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithOpenApi(LancaMovimentacaoDocumentacao.CreateSummay);

        return app;
    }
}