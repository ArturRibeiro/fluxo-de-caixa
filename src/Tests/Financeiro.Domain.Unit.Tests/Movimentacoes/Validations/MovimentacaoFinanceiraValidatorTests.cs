namespace Financeiro.Domain.Unit.Tests.Movimentacoes.Validations;

public class MovimentacaoFinanceiraValidatorTests
{
    private readonly MovimentacaoFinanceiraValidator _validator;

    public MovimentacaoFinanceiraValidatorTests() => _validator = new MovimentacaoFinanceiraValidator();

    [Theory(DisplayName = "Deve passar para movimentação válida")]
    [ClassData(typeof(MovimentacaoFinanceiraFaker))]
    public void Validar_MovimentacaoValida_DevePassar(MovimentacaoFinanceira movimentacao)
    {
        // Arrange
        
        // Act
        var resultado = _validator.Validate(movimentacao);

        // Assert
        resultado.IsValid.Should().BeTrue();
        resultado.Errors.Should().BeEmpty();
    }
}