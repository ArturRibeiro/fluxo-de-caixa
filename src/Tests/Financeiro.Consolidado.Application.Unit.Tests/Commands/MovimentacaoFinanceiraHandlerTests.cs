using Financeiro.Consolidado.Commands.Consolidados;
using Financeiro.Consolidado.Commands.Consolidados.Extensions;
using Microsoft.Extensions.Logging;
using TipoMovimentacao = Financeiro.Shared.TipoMovimentacao;

namespace Financeiro.Consolidado.Application.Unit.Tests.Commands;

public class MovimentacaoFinanceiraHandlerTests
{
    private readonly Mock<IPeriodoRepository> _repoMock;
    private readonly Mock<ILogger<MovimentacaoFinanceiraHandler>> _loggerMock;
    private readonly MovimentacaoFinanceiraHandler _handler;

    public MovimentacaoFinanceiraHandlerTests()
    {
        _repoMock = new Mock<IPeriodoRepository>();
        _loggerMock = new Mock<ILogger<MovimentacaoFinanceiraHandler>>();
        _handler = new MovimentacaoFinanceiraHandler(_repoMock.Object, _loggerMock.Object);
    }

    [Fact(DisplayName = "Deve consolidar movimentação com sucesso")]
    public async Task Handle_DeveConsolidarMovimentacao()
    {
        // Arrange
        var command = new ConsolidaMovimentacaoCommand(
            Data: new DateTime(2025, 11, 25),
            Valor: 150,
            TipoId: TipoMovimentacao.Debito.Value.GetValueOrDefault(),
            TipoNome: "Nome",
            Descricao: "Alguma coisa");

        var periodo = new Periodo(command.DataOnly);

        _repoMock.Setup(x => x.GetAsync(command.DataOnly))
            .ReturnsAsync(periodo);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().NotThrowAsync();
        _repoMock.Verify(x => x.GetAsync(command.DataOnly), Times.Once);
        _repoMock.Verify(x => x.AddAsync(It.Is<Periodo>(p =>
            p.Movimentacoes.Any(m => m.Descricao == command.Descricao && m.Valor == command.Valor))), Times.Once);
    }
    
    [Fact(DisplayName = "Deve criar novo período quando não existir")]
    public async Task Handle_DeveCriarPeriodo_QuandoNaoExistir()
    {
        // Arrange
        var command = new ConsolidaMovimentacaoCommand(
            Data: new DateTime(2025, 11, 25),
            Valor: 300,
            TipoId: TipoMovimentacao.Debito.Value.GetValueOrDefault(),
            TipoNome: "Nome",
            Descricao: "Entrada");

        var periodoCriado = new Periodo(command.DataOnly);
        _repoMock.Setup(x => x.GetAsync(command.DataOnly)).ReturnsAsync((Periodo)null);
        _repoMock.Setup(x => x.AddAsync(It.IsAny<Periodo>())).Returns(Task.CompletedTask);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().NotThrowAsync();
        _repoMock.Verify(x => x.GetAsync(command.DataOnly), Times.Once);
        _repoMock.Verify(x => x.AddAsync(It.IsAny<Periodo>()), Times.Once);
    }

}