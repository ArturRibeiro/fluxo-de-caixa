namespace Financeiro.Consolidado.Infrastructure.Repositorys;

public class PeriodoRepository : IPeriodoRepository
{
    private readonly ApplicationDbContext _context;

    public PeriodoRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task AddAsync(Periodo saldo)
    {
        if (saldo.Id is null) _context.Add(saldo);
        else _context.Update(saldo);
        await _context.SaveChangesAsync();
    }

    public async Task<Periodo?> GetAsync
        (DateOnly data) 
            => await _context.Saldos.FirstOrDefaultAsync(x => x.Data == data);
}