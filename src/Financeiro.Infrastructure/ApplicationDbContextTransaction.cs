namespace Financeiro.Infrastructure;

public partial class ApplicationDbContext
{
    private IDbContextTransaction dbContextTransaction;
    public async Task BeginTransactionAsync(CancellationToken cancellationToken)
    {
        dbContextTransaction = await Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken)
    {
        try
        {
            await SaveChangesAsync(cancellationToken);
            dbContextTransaction?.CommitAsync(cancellationToken);
        }
        catch
        {
            await RollbackTransactionAsync(cancellationToken);
            throw;
        }
        finally
        {
            if (dbContextTransaction != null) DisposeTransaction();
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken)
    {
        try
        {
            await dbContextTransaction?.RollbackAsync(cancellationToken);
        }
        finally
        {
            DisposeTransaction();
        }
    }
    
    private void DisposeTransaction()
    {
        try
        {
            dbContextTransaction.Dispose();
            dbContextTransaction = null;
        }
        catch
        {
            // Optionally handle or log any exceptions that occur during disposal.
        }
    }
}