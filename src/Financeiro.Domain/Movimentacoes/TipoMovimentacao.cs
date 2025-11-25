namespace Financeiro.Domain.Movimentacoes;

public class TipoMovimentacao : ValueObject
{
    public string Nome { get; }
    public int Codigo { get; }

    protected TipoMovimentacao(){}
    private TipoMovimentacao(int codigo, string nome)
    {
        Codigo = codigo;
        Nome = nome;
    }

    public static readonly TipoMovimentacao Credito = new(1, "Crédito");
    public static readonly TipoMovimentacao Debito = new(2, "Débito");

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Codigo;
        yield return Nome;
    }

    public static implicit operator TipoMovimentacao
        (int value)
        => FromCodigo(value);

    private static Func<int, TipoMovimentacao> FromCodigo = codigo => codigo switch
    {
        1 => Credito,
        2 => Debito,
        _ => throw new ArgumentException("Tipo de movimentação inválido.")
    };
}