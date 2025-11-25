namespace Financeiro.Application.Commands.Movimentacoes;

/// <summary>
/// Representa o comando para lançar uma movimentação financeira (crédito ou débito) no caixa diário.
/// </summary>
public record LancaMovimentacaoFinanceiraCommand : IRequest<Guid>
{
    /// <summary>
    /// Data em que a movimentação financeira ocorreu.
    /// </summary>
    [Required(ErrorMessage = "A data é obrigatória.")]
    public DateTime Data { get; set; }

    /// <summary>
    /// Valor da movimentação financeira. Deve ser maior que zero.
    /// </summary>
    [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero.")]
    public decimal Valor { get; set; }

    /// <summary>
    /// Tipo da movimentação financeira: Crédito (1) ou Débito (2).
    /// </summary>
    [EnumDataType(typeof(TipoMovimentacao), ErrorMessage = "Tipo de movimentação inválido.")]
    public TipoMovimentacao Tipo { get; set; }

    /// <summary>
    /// Descrição opcional da movimentação (ex.: venda, compra de insumo).
    /// </summary>
    [MaxLength(500, ErrorMessage = "A descrição deve conter no máximo 500 caracteres.")]
    public string? Descricao { get; set; }

    public Guid CorrelationId { get; set; }
}