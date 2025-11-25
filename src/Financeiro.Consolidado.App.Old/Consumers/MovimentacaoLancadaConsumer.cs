namespace Financeiro.Consolidado.App.Consumers;

public class MovimentacaoLancadaConsumer : IConsumer<MovimentacaoLancadaEvent>
{
    public async Task Consume(ConsumeContext<MovimentacaoLancadaEvent> context)
    {
        var evento = context.Message;
    }
}