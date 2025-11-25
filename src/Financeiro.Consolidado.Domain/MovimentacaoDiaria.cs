namespace Financeiro.Consolidado.Domain;

public class MovimentacaoDiaria
{
    public decimal Valor { get; private set; }
    public string Descricao { get; private set; }
    public TipoMovimentacao Tipo { get; private set; }

    protected MovimentacaoDiaria() { }
    
    public MovimentacaoDiaria(decimal valor, string descricao, TipoMovimentacao tipo) 
    {
        this.Valor = valor;
        this.Descricao = descricao;
        this.Tipo = tipo;
    }
}