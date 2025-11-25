namespace Financeiro.Infrastructure.BackgroundServices;

public partial class EventStorePublisherJob
{
        private static async Task<List<EventDataModel>> GetMovimentacoes
        (EventStoreDbContext db)
        => await db.Events
            .Where(e => !e.IsPublished)
            .OrderBy(e => e.OccurredOn)
            .Take(50)
            .ToListAsync();

    private static async Task<List<MovimentacaoLancadaEvent?>> ConveterMovimentacaoLancadaEvent
        (IEnumerable<EventDataModel> movimentacoes)
    {
        var result = movimentacoes
            .Select(x => JsonSerializer.Deserialize<MovimentacaoLancadaEvent>(x.EventData, _options))
            .ToList();

        return result;
    }

    private static async Task UpdateMovimentacoes(EventStoreDbContext db,
        List<EventDataModel> movimentacoes)
    {
        movimentacoes.ForEach(x => x.IsPublished = true);
        db.Events.UpdateRange(movimentacoes);
        await db.SaveChangesAsync();
    }
}