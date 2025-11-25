namespace Financeiro.Consolidado.Command.Queries.Consolidados;

public class MovimentacaoConsolidadaHandler(IApplicationDbContextReading contextReading)
    : IRequestHandler<MovimentacaoConsolidadaQuery, MovimentacaoConsolidadaResponse>
{
    public async Task<MovimentacaoConsolidadaResponse> Handle(MovimentacaoConsolidadaQuery request, CancellationToken cancellationToken)
    {
        var result =  await contextReading.DbSet<Periodo>()
            .Include(x => x.Movimentacoes)
            .FirstOrDefaultAsync(x => x.Data == request.Data);

        if (result is null) return new MovimentacaoConsolidadaResponse();
        
        var consolidadaResponse = new MovimentacaoConsolidadaResponse
        {
            Data = result.Data,
            Saldo = result.Saldo,
            TotaEntrada = result.TotalEntradas,
            TotalSaida = result.TotalSaidas,
            ConsolidadoDetalheModels = result.Movimentacoes.Select(x => new ConsolidadoDetalheResponse
            {
                Descricao = x.Descricao,
                Tipo = (TipoMovimentacao)x.Tipo.Value.GetValueOrDefault(),
                Valor = x.Valor
            }).ToList()
        };

        return consolidadaResponse;
    }
}