namespace Financeiro.Application.Commands.Behaviors;

[ExcludeFromCodeCoverage]
public class TransactionBehavior<TRequest, TResponse>(
    IApplicationDbContext applicationDbContext,
    ILogger<TransactionBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        TResponse response = default;
        try
        {
            logger.LogInformation("Begin Transaction : {Name}", typeof(TRequest).Name);
            await applicationDbContext.BeginTransactionAsync(cancellationToken);
            response = await next();
            await applicationDbContext.CommitTransactionAsync(cancellationToken);
            logger.LogInformation("End transaction : {Name}", typeof(TRequest).Name);
        }
        catch (Exception ex)
        {
            logger.LogInformation("Rollback transaction executed {Name}", typeof(TRequest).Name);
            await applicationDbContext.RollbackTransactionAsync(cancellationToken);
            logger.LogError(ex.Message, ex.StackTrace);
            throw;
        }

        return response;
    }
}