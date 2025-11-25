namespace Financeiro.Shared.Contracts;

public interface ConsolidadoAtualizado
{
    Guid LancamentoId { get; }
    DateTime Data { get; }
    decimal SaldoFinal { get; }
}