namespace Financeiro.Application.Commands.Movimentacoes;

public class MovimentacaoLancadaEventHandler(IEventStore eventStore,
    ILogger<MovimentacaoLancadaEventHandler> logger) 
    : INotificationHandler<MovimentacaoLancadaEvent>
{
    public async Task Handle(MovimentacaoLancadaEvent notification,
        CancellationToken cancellationToken) 
        => await eventStore.AppendAsync(notification.MovimentacaoId, notification, cancellationToken);
}