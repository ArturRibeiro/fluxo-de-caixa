using Financeiro.Shared;

namespace Financeiro.Consolidado.Infrastructure;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options), IApplicationDbContext
{
    private const string SCHEMA = "consolidados";

    public DbSet<Periodo> Saldos { get; set; }

    protected override void OnModelCreating
        (ModelBuilder modelBuilder)
        => modelBuilder.HasDefaultSchema(SCHEMA)
            .ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    
    public void Seed(IConfiguration app)
    {
        if (app.GetSection("Environment").Value == "Test") return;
        this.Database.EnsureDeleted();
        this.Database.EnsureCreated();
        
        // var periodo = Periodo.Criar(DateOnly.FromDateTime(DateTime.Now));
        // periodo.AdicionarMovimentacao(100, "Salário 1", TipoMovimentacao.Credito);
        // periodo.AdicionarMovimentacao(963, "Venda 3", TipoMovimentacao.Debito);
        //
        // this.Saldos.Add(periodo);
        //
        // this.SaveChanges();
    }
}