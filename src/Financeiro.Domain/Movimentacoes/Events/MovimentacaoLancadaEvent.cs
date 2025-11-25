namespace Financeiro.Domain.Movimentacoes.Events;

public record MovimentacaoLancadaEvent(
    Guid MovimentacaoId,
    DateTime Data,
    decimal Valor,
    int TipoId,
    string TipoNome,
    string Descricao
) : IDomainNotification;
