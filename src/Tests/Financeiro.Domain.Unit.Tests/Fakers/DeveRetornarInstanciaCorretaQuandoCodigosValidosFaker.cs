namespace Financeiro.Domain.Unit.Tests.Fakers;

public class DeveRetornarInstanciaCorretaQuandoCodigosValidosFaker : TheoryData<int, TipoMovimentacao>
{
    public DeveRetornarInstanciaCorretaQuandoCodigosValidosFaker()
    {
        Add(1, TipoMovimentacao.Credito);
        Add(2, TipoMovimentacao.Debito);
    }
}