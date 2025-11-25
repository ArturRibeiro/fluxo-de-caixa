namespace Financeiro.Consolidado.Domain;

public interface IPeriodoRepository
{
    Task AddAsync(Periodo saldo);
    Task<Periodo?> GetAsync(DateOnly data);
}