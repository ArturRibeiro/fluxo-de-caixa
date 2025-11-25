using Financeiro.Domain.Movimentacoes.Events;
using Financeiro.Shared;
using Microsoft.Extensions.Logging;

namespace Financeiro.Application.Unit.Tests.Commads;

public class MovimentacaoLancadaEventHandlerTests
{
    private readonly Mock<IEventStore> _eventStoreMock;
    private readonly Mock<ILogger<MovimentacaoLancadaEventHandler>> _loggerMock;
    private readonly MovimentacaoLancadaEventHandler _handler;

    public MovimentacaoLancadaEventHandlerTests()
    {
        _eventStoreMock = new Mock<IEventStore>();
        _loggerMock = new Mock<ILogger<MovimentacaoLancadaEventHandler>>();
        _handler = new MovimentacaoLancadaEventHandler(_eventStoreMock.Object, _loggerMock.Object);
    }

    [Fact(DisplayName = "Deve gravar evento no EventStore com sucesso")]
    public async Task Handle_DevePersistirEvento()
    {
        // Arrange
        var evento = new MovimentacaoLancadaEvent(
            Guid.NewGuid(),
            DateTime.UtcNow,
            100,
            TipoId: 1,
            TipoNome: "Crédito",
            Descricao: "Venda realizada");

        // Act
        Func<Task> act = async () => 
            await _handler.Handle(evento, CancellationToken.None);

        // Assert
        await act.Should().NotThrowAsync();
        _eventStoreMock.Verify(x => 
            x.AppendAsync(evento.MovimentacaoId, evento, It.IsAny<CancellationToken>()), Times.Once);
    }
}