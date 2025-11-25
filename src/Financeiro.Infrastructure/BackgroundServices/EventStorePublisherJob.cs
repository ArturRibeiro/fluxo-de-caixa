namespace Financeiro.Infrastructure.BackgroundServices;

public partial class EventStorePublisherJob(
    ILogger<EventStorePublisherJob> logger,
    IServiceProvider serviceProvider,
    IMessagingPublisher messagingPublisher)
    : IJob
{
    private static JsonSerializerOptions _options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = false
    };

    public async Task Execute(IJobExecutionContext context)
    {
        logger.LogInformation("Iniciando leitura do EventStore...");

        using var scope = serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<EventStoreDbContext>();
        
        var movimentacoes = await GetMovimentacoes(db);
        
        if (movimentacoes.Count == 0)
        {
            logger.LogInformation("Nenhum evento encontrado.");
            return;
        }
        
        var movimentacoesEvents = await ConveterMovimentacaoLancadaEvent(movimentacoes);
        await messagingPublisher.PublishAsync(movimentacoesEvents);
        await UpdateMovimentacoes(db, movimentacoes);
    }
}