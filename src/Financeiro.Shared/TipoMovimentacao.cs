namespace Financeiro.Shared;

public class TipoMovimentacao : ValueObject
{
    public static readonly TipoMovimentacao Credito = new TipoMovimentacao(1, "Crédito");
    public static readonly TipoMovimentacao Debito = new TipoMovimentacao(2, "Débito");
    public string Descricao { get; }
    public int? Value { get; }

    protected TipoMovimentacao(){}
    private TipoMovimentacao(int value, string nome)
    {
        Value = value;
        Descricao = nome;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
        yield return Descricao;
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