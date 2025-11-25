namespace Financeiro.Application.Commands.Behaviors;

[ExcludeFromCodeCoverage]
public class ApplicationException : ValidationException
{
    public IReadOnlyList<ValidationResult> ValidationResults { get; }

    public ApplicationException(IEnumerable<ValidationResult> validationResults)
        : base("Ocorreram erros de validação.")
    {
        ValidationResults = validationResults.ToList();
    }

}