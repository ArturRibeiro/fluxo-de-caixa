namespace Financeiro.Domain.Unit.Tests.Movimentacoes;

public class TipoMovimentacaoTests
{
    [Theory(DisplayName = "Deve retornar a instância correta a partir do código")]
    [ClassData(typeof(DeveRetornarInstanciaCorretaQuandoCodigosValidosFaker))]
    public void DeveRetornarInstanciaCorreta_QuandoCodigoValido(
        int codigo,
        TipoMovimentacao esperado)
    {
        // Act
        TipoMovimentacao tipo = codigo;

        // Assert
        tipo.Should().Be(esperado);
        tipo.Codigo.Should().Be(codigo);
    }

    [Theory(DisplayName = "Deve falhar ao tentar criar TipoMovimentacao com código inválido")]
    [ClassData(typeof(DeveFalharQuandoCodigosInvalidosFaker))]
    public void DeveFalhar_QuandoCodigoInvalido(int codigo)
    {
        // Act
        var act = () => { TipoMovimentacao tipo = codigo; };

        // Assert
        act.Should()
            .Throw<ArgumentException>()
            .WithMessage("Tipo de movimentação inválido.*");
    }

    [Fact(DisplayName = "Instâncias diferentes devem ser diferentes (Value Object)")]
    public void DeveSerDiferente_QuandoInstanciasDiferentes()
    {
        TipoMovimentacao.Credito.Should().NotBe(TipoMovimentacao.Debito);
        TipoMovimentacao.Credito.Equals(TipoMovimentacao.Debito).Should().BeFalse();
    }
}