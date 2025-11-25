namespace Financeiro.Shared.Contracts;

public class MovimentacaoLancadaEvent
{
    public Guid LancamentoId { get; set; }
    public DateTime Data { get; set; }
    public decimal Valor { get; set; }
    public int TipoId { get; set; }
    public string TipoNome { get; set; }
    public string Descricao { get; set; }
}