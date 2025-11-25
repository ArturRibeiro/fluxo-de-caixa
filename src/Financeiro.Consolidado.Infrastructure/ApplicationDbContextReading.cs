using Financeiro.Consolidado.Command.Queries;

namespace Financeiro.Consolidado.Infrastructure;

public sealed class ApplicationDbContextReading : DbContext, IApplicationDbContextReading
{
    private const string SCHEMA = "consolidados";

    public ApplicationDbContextReading(DbContextOptions<ApplicationDbContextReading> options) : base(options)
    {
        this.ChangeTracker.AutoDetectChangesEnabled = false;
        this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }
    
    public IQueryable<TEntity> DbSet<TEntity>()
        where TEntity : class
        => this.Set<TEntity>().AsNoTracking();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(SCHEMA);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContextReading).Assembly);
    }
}