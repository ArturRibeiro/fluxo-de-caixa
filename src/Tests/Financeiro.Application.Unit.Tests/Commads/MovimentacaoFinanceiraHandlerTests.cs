namespace Financeiro.Application.Unit.Tests.Commads;

public class MovimentacaoFinanceiraHandlerTests
{
    private readonly Mock<IMovimentacaoFinanceiraRepository> _repoMock;
    private readonly MovimentacaoFinanceiraHandler _handler;

    public MovimentacaoFinanceiraHandlerTests()
    {
        _repoMock = new Mock<IMovimentacaoFinanceiraRepository>();
        _handler = new MovimentacaoFinanceiraHandler(_repoMock.Object);
    }

    [Fact(DisplayName = "Deve lançar movimentação com sucesso")]
    public async Task Handle_DeveLancarMovimentacaoComSucesso()
    {
        // Arrange
        var command = new LancaMovimentacaoFinanceiraCommand
        {
            Valor = 200,
            Tipo = TipoMovimentacao.Credito,
            Descricao = "Venda"
        };

        // Act
        var resultado = await _handler.Handle(command, CancellationToken.None);

        // Assert
        resultado.Should().NotBeEmpty(); // Guid válido

        _repoMock.Verify(x => x.Add(It.IsAny<MovimentacaoFinanceira>()), Times.Once);
        _repoMock.Verify(x => x.Save(It.IsAny<CancellationToken>()), Times.Once);
    }
}
