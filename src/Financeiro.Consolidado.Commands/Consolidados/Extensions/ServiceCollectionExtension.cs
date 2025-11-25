using System.Diagnostics.CodeAnalysis;

namespace Financeiro.Consolidado.Commands.Consolidados.Extensions;

[ExcludeFromCodeCoverage]
public static class ServiceCollectionExtension
{
    public static Periodo ConvertSaldo
        (this ConsolidaMovimentacaoCommand command) =>
        Periodo.Criar(data: command.DataOnly);
}