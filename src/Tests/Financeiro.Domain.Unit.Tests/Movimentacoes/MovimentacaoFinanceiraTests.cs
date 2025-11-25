namespace Financeiro.Domain.Unit.Tests.Movimentacoes;

public class MovimentacaoFinanceiraTests
{
    [Theory(DisplayName = "Deve construir MovimentacaoFinanceira corretamente para todos os cenários válidos")]
    [ClassData(typeof(DeveConstruirMovimentacaoFinanceiraCorretamenteFaker))]
    public void DeveConstruirMovimentacaoFinanceiraCorretamente(decimal valor, TipoMovimentacao tipo, string? descricao)
    {
        // Act
        var entidade = MovimentacaoFinanceira.Criar(valor, tipo, descricao);

        // Assert
        entidade.Should().NotBeNull();
        entidade.Id.Should().Be(Guid.Empty);

        entidade.Data.Date.Should().Be(DateTime.Now.Date);
        entidade.Valor.Should().Be(valor);
        entidade.Tipo.Should().Be(tipo);
        entidade.Descricao.Should().Be(descricao);
    }
}