namespace Financeiro.Consolidado.Web.Api.EndPoints.Documentacao;

internal class MovimentacaoConsolidadaDocumentacao
{
    public static OpenApiOperation CreateSummay(OpenApiOperation op)
    {
        op.Summary = "Serviço do consolidado diário de movimentação financeira";
        op.Description = "";
        return op;
    }
}