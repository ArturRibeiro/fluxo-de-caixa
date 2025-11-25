namespace Financeiro.Infrastructure.Repositorys;

public class MovimentacaoFinanceiraRepository(ApplicationDbContext context) : IMovimentacaoFinanceiraRepository
{
    public void Add
        (MovimentacaoFinanceira movimentacaoFinanceira) 
            => context.MovimentacaoFinanceiras.Add(movimentacaoFinanceira);

    public async Task Save(CancellationToken cancellationToken)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}