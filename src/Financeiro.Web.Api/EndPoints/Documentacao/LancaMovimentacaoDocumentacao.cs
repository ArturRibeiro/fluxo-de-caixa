namespace Financeiro.Web.Api.EndPoints.Documentacao;

internal class LancaMovimentacaoDocumentacao
{
    public static OpenApiOperation CreateSummay(OpenApiOperation op)
    {
        op.Summary = "Lança uma nova movimentação financeira";
        op.Description = """
                         Lança uma nova movimentação financeira no caixa.

                         Requer os seguintes campos no corpo da requisição (JSON):

                         - `Data` (DateTime, obrigatório): Data em que a movimentação financeira ocorreu..
                         - `Valor` (Decimal, obrigatório): Valor da movimentação financeira. Deve ser maior que zero..
                         - `Tipo` (TipoMovimentacao, obrigatório): Tipo da movimentação financeira: Crédito (1) ou Débito (2)..
                         - `Descricao` (string, obrigatório): Descrição opcional da movimentação (ex.: venda, compra de insumo).

                         A API retorna 200 OK em caso de sucesso ou 400 Bad Request se os dados forem inválidos.
                         """;
        
        op.RequestBody = new OpenApiRequestBody
        {
            Required = true,
            Content = {
                ["application/json"] = new OpenApiMediaType
                {
                    Schema = new OpenApiSchema
                    {
                        Type = "object",
                        Properties = {
                            ["data"] = new OpenApiSchema { Type = "Date time", Format = "date-time" },
                            ["valor"] = new OpenApiSchema { Type = "decimal", Format = "decimal" },
                            ["tipo"] = new OpenApiSchema { Type = "enum", Format = "enum" },
                            ["descricao"] = new OpenApiSchema { Type = "string" }
                        }
                    }
                }
            }
        };

        return op;
    }
}