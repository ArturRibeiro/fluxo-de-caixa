namespace Financeiro.Consolidado.Command.Queries.Consolidados.Responses;

public class ConsolidadoDetalheResponse
{
    public decimal Valor { get; set; }
    public TipoMovimentacao Tipo { get; set; }
    public string? Descricao { get; set; }
}