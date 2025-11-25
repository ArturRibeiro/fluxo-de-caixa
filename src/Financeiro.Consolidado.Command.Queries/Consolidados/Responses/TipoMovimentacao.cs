namespace Financeiro.Consolidado.Command.Queries.Consolidados.Responses;

public enum TipoMovimentacao : int
{
    [Display(Name = "Crédito")] Credito = 1,
    [Display(Name = "Débito")] Debito = 2
}