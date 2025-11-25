using System.ComponentModel.DataAnnotations;

namespace Financeiro.Consolidado.App.Models;

public class ConsolidadoModel
{
    public DateOnly Data { get; set; }
    public decimal TotaEntrada { get; set; }
    public decimal TotalSaida { get; set; }
    public decimal Saldo { get; set; }
    public List<ConsolidadoDetalheModel> ConsolidadoDetalheModels { get; set; } = new();
}

public class ConsolidadoDetalheModel
{
    public decimal Valor { get; set; }
    public TipoMovimentacao Tipo { get; set; }
    public string? Descricao { get; set; }
}
    
public enum TipoMovimentacao
{
    [Display(Name = "Crédito")] Credito = 1,
    [Display(Name = "Débito")] Debito = 2
}