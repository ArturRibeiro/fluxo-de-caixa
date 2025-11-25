namespace Financeiro.Consolidado.Domain;

public class Periodo
{
    private readonly List<MovimentacaoDiaria> _movimentacoes = new();
    
    public long? Id { get; private set; }
    
    public DateOnly Data { get; private set; }
    public IReadOnlyCollection<MovimentacaoDiaria> Movimentacoes => _movimentacoes;

    public decimal TotalEntradas { get; private set; }
    public decimal TotalSaidas { get; private set; }
    public decimal Saldo { get; private set; }

    protected Periodo() { }

    public Periodo(DateOnly data) => Data = data;

    public void AdicionarMovimentacao(decimal valor, string descricao, TipoMovimentacao tipo)
    {
        _movimentacoes.Add(new MovimentacaoDiaria(valor, descricao, tipo));
        
        TotalEntradas = _movimentacoes.Where(m => m.Tipo.Value == TipoMovimentacao.Credito.Value).Sum(m => m.Valor);
        TotalSaidas = _movimentacoes.Where(m => m.Tipo.Value == TipoMovimentacao.Debito.Value).Sum(m => m.Valor);
        Saldo = TotalEntradas - TotalSaidas;
    }

    public static Periodo Criar(DateOnly data) => new(data);
}