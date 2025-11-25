namespace Financeiro.Domain.Movimentacoes.Interfaces;

public interface IMovimentacaoFinanceiraRepository
{
    void Add(MovimentacaoFinanceira movimentacaoFinanceira);
    Task Save(CancellationToken cancellationToken);
}