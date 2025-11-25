namespace Financeiro.App.Models;

public class LancamentoInputModel
{
    [Required(ErrorMessage = "O valor do lançamento é obrigatório.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero.")]
    public decimal Valor { get; set; }

    [Required(ErrorMessage = "O tipo da movimentação é obrigatório.")]
    [EnumDataType(typeof(TipoMovimentacao), ErrorMessage = "Tipo de movimentação inválido.")]
    public TipoMovimentacao Tipo { get; set; }

    [Required(ErrorMessage = "A descrição é obrigatória.")]
    [StringLength(200, ErrorMessage = "A descrição deve ter no máximo 200 caracteres.")]
    public string? Descricao { get; set; }
}
    
public enum TipoMovimentacao
{
    [Display(Name = "Crédito")] Credito = 1,
    [Display(Name = "Débito")] Debito = 2
}