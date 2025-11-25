using MockQueryable.Moq;

namespace Financeiro.Consolidado.Application.Unit.Tests.Queries;

public class MovimentacaoConsolidadaHandlerTests
{
    private readonly Mock<IApplicationDbContextReading> _contextMock;
    private readonly MovimentacaoConsolidadaHandler _handler;

    public MovimentacaoConsolidadaHandlerTests()
    {
        _contextMock = new Mock<IApplicationDbContextReading>();
        _handler = new MovimentacaoConsolidadaHandler(_contextMock.Object);
    }

    [Fact(DisplayName = "Deve retornar consolidado quando o período for encontrado")]
    public async Task Handle_DeveRetornarConsolidado_QuandoPeriodoExistir()
    {
        // Arrange
        var data = new DateOnly(2025, 11, 25);
        var periodo = new Periodo(data);
        periodo.AdicionarMovimentacao(100, "Venda", TipoMovimentacao.Credito);
        periodo.AdicionarMovimentacao(30, "Compra", TipoMovimentacao.Debito);

        var mockDbSet = new List<Periodo> { periodo }.BuildMockDbSet();
        _contextMock.Setup(c => c.DbSet<Periodo>()).Returns(mockDbSet.Object);

        var query = new MovimentacaoConsolidadaQuery(data);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Saldo.Should().Be(70);
        result.TotaEntrada.Should().Be(100);
        result.TotalSaida.Should().Be(30);
        result.ConsolidadoDetalheModels.Should().HaveCount(2);
    }
    
    [Fact(DisplayName = "Deve retornar vazio quando o período não for encontrado")]
    public async Task Handle_DeveRetornarVazio_QuandoPeriodoNaoExistir()
    {
        // Arrange
        var mockDbSet = new List<Periodo>().BuildMockDbSet();
        _contextMock.Setup(c => c.DbSet<Periodo>()).Returns(mockDbSet.Object);

        var query = new MovimentacaoConsolidadaQuery(new DateOnly(2025, 11, 25));

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
    }
}
