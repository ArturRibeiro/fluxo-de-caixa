namespace Financeiro.Consolidado.Commands.Consolidados;

public record ConsolidaMovimentacaoCommand(
    //Guid MovimentacaoId,
    DateTime Data,
    decimal Valor,
    int TipoId,
    string TipoNome,
    string Descricao) : IRequest
{
    public DateOnly DataOnly => DateOnly.FromDateTime(Data);
}
