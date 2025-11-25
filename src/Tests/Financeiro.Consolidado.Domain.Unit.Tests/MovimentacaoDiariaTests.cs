using Financeiro.Shared;
using FluentAssertions;

namespace Financeiro.Consolidado.Domain.Unit.Tests;

public class MovimentacaoDiariaTests
{
    [Fact(DisplayName = "Deve criar movimentação com dados válidos")]
    public void Criar_Movimentacao_DevePopularCamposCorretamente()
    {
        // Arrange
        decimal valor = 150.75m;
        string descricao = "Pagamento de serviço";
        var tipo = TipoMovimentacao.Credito;

        // Act
        var movimentacao = new MovimentacaoDiaria(valor, descricao, tipo);

        // Assert
        movimentacao.Valor.Should().Be(valor);
        movimentacao.Descricao.Should().Be(descricao);
        movimentacao.Tipo.Should().Be(tipo);
    }
}
