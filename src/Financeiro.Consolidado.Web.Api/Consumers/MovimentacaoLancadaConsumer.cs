namespace Financeiro.Consolidado.Web.Api.Consumers;

public class MovimentacaoLancadaConsumer(IMediator mediator, ILogger<MovimentacaoLancadaEvent> logger) : IConsumer<MovimentacaoLancadaEvent>
{
    public async Task Consume(ConsumeContext<MovimentacaoLancadaEvent> context)
    {
        var msg = context.Message;

        logger.LogInformation("Processando AtualizarConsolidado: LancamentoId={LancamentoId}, Data={Data}, Valor={Valor}, Tipo={TipoId}, Tipo={TipoNome}, Tipo={Descricao}",
            msg.LancamentoId, msg.Data, msg.Valor, msg.TipoId, msg.TipoNome, msg.Descricao);

        var command = new ConsolidaMovimentacaoCommand(
            Data: context.Message.Data
            , Valor: context.Message.Valor
            , TipoId: context.Message.TipoId
            , TipoNome: context.Message.TipoNome
            , Descricao: context.Message.Descricao);
        await mediator.Send(command);

        logger.LogInformation("ConsolidadoAtualizado publicado: LancamentoId={LancamentoId}, Data={Data}", msg.LancamentoId, msg.Data);
    }
}