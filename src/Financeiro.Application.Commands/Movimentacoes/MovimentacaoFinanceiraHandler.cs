namespace Financeiro.Application.Commands.Movimentacoes;

public class MovimentacaoFinanceiraHandler(IMovimentacaoFinanceiraRepository movimentacaoFinanceiraRepository)
    : IRequestHandler<LancaMovimentacaoFinanceiraCommand, Guid>
{
    public async Task<Guid> Handle(
        LancaMovimentacaoFinanceiraCommand command,
        CancellationToken cancellationToken)
    {
        var movimentacaoFinanceira = MovimentacaoFinanceira.Criar(valor: command.Valor, tipo: (int)command.Tipo, descricao: command.Descricao);
        movimentacaoFinanceiraRepository.Add(movimentacaoFinanceira);
        await movimentacaoFinanceiraRepository.Save(cancellationToken);
        return Guid.NewGuid();
    }
}