namespace Financeiro.SpecFlow.Integration.Tests.Steps;

[Binding]
public class LancarMovimentacaoSteps
{
    private readonly LancamentosWebApplicationFactory _factory;
    private readonly ScenarioContext _scenarioContext;

    public LancarMovimentacaoSteps(LancamentosWebApplicationFactory factory, ScenarioContext scenarioContext)
    {
        _factory = factory;
        _scenarioContext = scenarioContext;
    }
    
    [Given(@"um lançamento de movimentação financeira")]
    public async Task DadoUmLancamentoDeMovimentacaoFinanceira()
    {
        var dbContext = _factory.ApplicationDbContext;
        dbContext.Should().NotBeNull();
        dbContext.MovimentacaoFinanceiras.ToList().Should().HaveCount(1);
    }

    [Given(@"informo o tipo de movimentação ""([^""]*)""")]
    public async Task DadoInformoOTipoDeMovimentacao(string tipo) => _scenarioContext.Add("TipoMovimentacao", tipo);

    [Given(@"informo o valor ""([^""]*)""")]
    public async Task DadoInformoOValor(string valor) => _scenarioContext.Add("Valor", valor);

    [Given(@"informo a descricao ""([^""]*)""")]
    public async Task InformoADescricao(string descricao) => _scenarioContext.Add("Descricao", descricao);

    [Then(@"o sistema deve registrar a entrada corretamente")]
    public async Task EntaoOSistemaDeveRegistrarAEntradaCorretamente()
    {
        var command = new LancaMovimentacaoFinanceiraCommand();
        command.Tipo = _scenarioContext.Get<string>("TipoMovimentacao") == "Crédito" ? TipoMovimentacao.Credito : TipoMovimentacao.Debito;
        command.Valor = decimal.Parse(_scenarioContext.Get<string>("Valor"));
        command.Descricao = _scenarioContext.Get<string>("Descricao");
        
        var result = await _factory.SendAsync<Result>(command, "movimentacao");
        result.Should().NotBeNull();
        var dbContext = _factory.ApplicationDbContext;
        dbContext.Should().NotBeNull();
        dbContext.MovimentacaoFinanceiras.ToList().Should().HaveCountGreaterThanOrEqualTo(1);
    }
}
