namespace Financeiro.Consolidado.Command.Queries.Consolidados;

public record class MovimentacaoConsolidadaQuery (DateOnly Data): IRequest<MovimentacaoConsolidadaResponse>
{
}