using Financeiro.Shared;
using FluentAssertions;

namespace Financeiro.Consolidado.Domain.Unit.Tests;

public class PeriodoTests
{
    [Fact(DisplayName = "Deve criar período com a data correta")]
    public void Criar_DeveCriarPeriodoComData()
    {
        // Arrange
        var data = new DateOnly(2025, 11, 25);

        // Act
        var periodo = Periodo.Criar(data);

        // Assert
        periodo.Data.Should().Be(data);
        periodo.Movimentacoes.Should().BeEmpty();
        periodo.TotalEntradas.Should().Be(0);
        periodo.TotalSaidas.Should().Be(0);
        periodo.Saldo.Should().Be(0);
    }

    [Fact(DisplayName = "Deve adicionar movimentação de entrada e atualizar totais")]
    public void Adicionar_Entrada_DeveAtualizarEntradasESaldo()
    {
        // Arrange
        var periodo = Periodo.Criar(new DateOnly(2025, 11, 25));

        // Act
        periodo.AdicionarMovimentacao(100, "Receita", TipoMovimentacao.Credito);

        // Assert
        periodo.TotalEntradas.Should().Be(100);
        periodo.TotalSaidas.Should().Be(0);
        periodo.Saldo.Should().Be(100);
        periodo.Movimentacoes.Should().HaveCount(1);
    }

    [Fact(DisplayName = "Deve adicionar movimentação de saída e atualizar totais")]
    public void Adicionar_Saida_DeveAtualizarSaidasESaldo()
    {
        // Arrange
        var periodo = Periodo.Criar(new DateOnly(2025, 11, 25));

        // Act
        periodo.AdicionarMovimentacao(30, "Despesa", TipoMovimentacao.Debito);

        // Assert
        periodo.TotalEntradas.Should().Be(0);
        periodo.TotalSaidas.Should().Be(30);
        periodo.Saldo.Should().Be(-30);
        periodo.Movimentacoes.Should().HaveCount(1);
    }

    [Fact(DisplayName = "Deve calcular corretamente com múltiplas movimentações")]
    public void Adicionar_MultiplasMovimentacoes_DeveAtualizarTotais()
    {
        // Arrange
        var periodo = Periodo.Criar(new DateOnly(2025, 11, 25));

        // Act
        periodo.AdicionarMovimentacao(200, "Venda", TipoMovimentacao.Credito);
        periodo.AdicionarMovimentacao(50, "Compra", TipoMovimentacao.Debito);
        periodo.AdicionarMovimentacao(30, "Serviço", TipoMovimentacao.Debito);

        // Assert
        periodo.TotalEntradas.Should().Be(200);
        periodo.TotalSaidas.Should().Be(80);
        periodo.Saldo.Should().Be(120);
        periodo.Movimentacoes.Should().HaveCount(3);
    }
}
