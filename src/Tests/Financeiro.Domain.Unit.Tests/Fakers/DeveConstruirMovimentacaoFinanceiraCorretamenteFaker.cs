namespace Financeiro.Domain.Unit.Tests.Fakers;

public class DeveConstruirMovimentacaoFinanceiraCorretamenteFaker : TheoryData<decimal, TipoMovimentacao, string?>
{
    public DeveConstruirMovimentacaoFinanceiraCorretamenteFaker()
    {
        Add(40m, TipoMovimentacao.Debito, "Descrição longa de teste para movimentação financeira");
    }
}