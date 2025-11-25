using Financeiro.Domain.Movimentacoes;
using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;
using Testcontainers.RabbitMq;
using TipoMovimentacao = Financeiro.Domain.Movimentacoes.TipoMovimentacao;

namespace Financeiro.SpecFlow.Integration.Tests.Hooks;

[Binding]
public class Hook
{
    private static LancamentosWebApplicationFactory Factory => new LancamentosWebApplicationFactory();
    // private static ConsolidadoWebApplicationFactory FactoryConsolidado => new ConsolidadoWebApplicationFactory();

    private readonly PostgreSqlContainer _postgreSqlContainer = new PostgreSqlBuilder()
        .WithImage("postgres:alpine")
        .WithDatabase("lancamentos")
        .WithUsername("postgres")
        .WithPassword("password")
        .WithAutoRemove(true)
        .Build();
    
    private readonly RabbitMqContainer _rabbitMqContainer = new RabbitMqBuilder().Build();
    
    public static string ConnectionString;

    [BeforeScenario]
    public async Task BeforeScenario()
    {
        await _rabbitMqContainer.StartAsync().ConfigureAwait(false);
        await _postgreSqlContainer.StartAsync();
        
        Console.WriteLine($"RabbitMqContainer:DefaultConnection: {_rabbitMqContainer.GetConnectionString()}");
        Console.WriteLine($"PostgreSqlContainer:DefaultConnection: {_postgreSqlContainer.GetConnectionString()}");
        
        using var scope1 = Factory.Services.CreateScope();
        var context = scope1.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        
        var list = new List<MovimentacaoFinanceira>() { MovimentacaoFinanceira.Criar(valor: 1, tipo: TipoMovimentacao.Credito, "Descricao 1") };
        context.MovimentacaoFinanceiras.AddRange(list);
        (context.SaveChanges() > 0).Should().BeTrue();
    }

    [AfterScenario]
    public async Task AfterScenario()
    {
        await _postgreSqlContainer.DisposeAsync();
        _rabbitMqContainer.DisposeAsync();
    }
}