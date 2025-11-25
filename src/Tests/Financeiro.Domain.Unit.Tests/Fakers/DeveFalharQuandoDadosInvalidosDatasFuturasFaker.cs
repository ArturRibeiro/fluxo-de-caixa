namespace Financeiro.Domain.Unit.Tests.Fakers;

public class DeveFalharQuandoDadosInvalidosDatasFuturasFaker : TheoryData<decimal, TipoMovimentacao, string?>
{
    public DeveFalharQuandoDadosInvalidosDatasFuturasFaker()
    {
        Add(100m, TipoMovimentacao.Credito, "Data futura +1 dia");
        Add(200m, TipoMovimentacao.Debito, "Data futura +1 mês");
        Add(300m, TipoMovimentacao.Credito, "Data futura +1 ano");
    }
}