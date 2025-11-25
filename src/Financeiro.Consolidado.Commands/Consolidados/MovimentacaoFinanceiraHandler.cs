namespace Financeiro.Consolidado.Commands.Consolidados;

public class MovimentacaoFinanceiraHandler(IPeriodoRepository periodoRepository, ILogger<MovimentacaoFinanceiraHandler> logger)
    : IRequestHandler<ConsolidaMovimentacaoCommand>
{
    public async Task Handle(
        ConsolidaMovimentacaoCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation("Iniciando a consolidação");
            var result = await periodoRepository.GetAsync(command.DataOnly);
            var periodo = result ?? command.ConvertSaldo();
            periodo.AdicionarMovimentacao(command.Valor, command.Descricao, command.TipoId);
            await periodoRepository.AddAsync(periodo);
            logger.LogInformation("Finalizando a consolidação");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao consolidação");
            throw;
        }
    }
}