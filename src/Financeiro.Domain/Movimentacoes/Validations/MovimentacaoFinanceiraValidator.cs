namespace Financeiro.Domain.Movimentacoes.Validations;

public class MovimentacaoFinanceiraValidator 
    : AbstractValidator<MovimentacaoFinanceira>
{
    public MovimentacaoFinanceiraValidator()
    {
        RuleFor(m => m.Valor)
            .GreaterThan(0).WithMessage("O valor deve ser maior que zero.");

        RuleFor(m => m.Tipo)
            .NotNull().WithMessage("O tipo da movimentação é obrigatório.");

        RuleFor(m => m.Descricao)
            .MinimumLength(3).When(m => !string.IsNullOrWhiteSpace(m.Descricao))
            .WithMessage("A descrição deve ter pelo menos 3 caracteres.");
    }
}