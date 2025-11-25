namespace Financeiro.Domain.Movimentacoes;

public class MovimentacaoFinanceira : AggregateRoot
{
    public Guid Id { get; private set; }
    public DateTime Data { get; private set; }
    public decimal Valor { get; private set; }
    public TipoMovimentacao Tipo { get; private set; }
    public string? Descricao { get; private set; }

    protected MovimentacaoFinanceira() { }
    
    private MovimentacaoFinanceira(decimal valor, TipoMovimentacao tipo, string? descricao = null)
    {
        Data = DateTime.Now;
        Valor = valor;
        Tipo = tipo;
        Descricao = descricao;

        this.AddEvent(new MovimentacaoLancadaEvent(
            Id,
            Data,
            Valor,
            TipoId: Tipo.Codigo,
            TipoNome: Tipo.Nome,
            Descricao: descricao));
    }
    
    public static MovimentacaoFinanceira 
        Criar(decimal valor, TipoMovimentacao tipo, string descricao) 
        => new(valor, tipo, descricao);
}