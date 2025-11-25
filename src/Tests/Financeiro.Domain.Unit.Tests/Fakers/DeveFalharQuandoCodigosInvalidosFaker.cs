namespace Financeiro.Domain.Unit.Tests.Fakers;

public class DeveFalharQuandoCodigosInvalidosFaker : TheoryData<int>
{
    public DeveFalharQuandoCodigosInvalidosFaker()
    {
        Add(0);
        Add(-1);
        Add(999);
        Add(3);
    }
}