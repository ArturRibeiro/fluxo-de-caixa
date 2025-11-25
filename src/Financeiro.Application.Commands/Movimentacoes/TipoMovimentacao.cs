namespace Financeiro.Application.Commands.Movimentacoes;

public enum TipoMovimentacao
{
    [Display(Name = "Crédito")] Credito = 1,
    [Display(Name = "Débito")] Debito = 2
}