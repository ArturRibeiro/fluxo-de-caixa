namespace Financeiro.Domain.Unit.Tests.Fakers;

public class MovimentacaoFinanceiraFaker
    : TheoryData<MovimentacaoFinanceira>
{
    public MovimentacaoFinanceiraFaker()
    {
        Add(MovimentacaoFinanceira.Criar(1, TipoMovimentacao.Credito, "Compra"));
        Add(MovimentacaoFinanceira.Criar(1, TipoMovimentacao.Debito, "Pagamento"));
    }
}
