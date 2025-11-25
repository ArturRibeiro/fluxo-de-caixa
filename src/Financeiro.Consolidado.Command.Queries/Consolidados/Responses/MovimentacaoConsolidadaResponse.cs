namespace Financeiro.Consolidado.Command.Queries.Consolidados.Responses;

public class MovimentacaoConsolidadaResponse
{
    public DateOnly Data { get; set; }
    public decimal TotaEntrada { get; set; }
    public decimal TotalSaida { get; set; }
    public decimal Saldo { get; set; }

    public List<ConsolidadoDetalheResponse> ConsolidadoDetalheModels { get; set; } = new();
}