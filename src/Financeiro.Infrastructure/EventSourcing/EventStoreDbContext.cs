namespace Financeiro.Infrastructure.EventSourcing;

public class EventStoreDbContext : DbContext
{
    private const string SCHEMA = "lancamentos";
    
    public EventStoreDbContext(DbContextOptions<EventStoreDbContext> options)
        : base(options)
    {
    }

    public DbSet<EventDataModel> Events => Set<EventDataModel>();

    protected override void OnModelCreating
        (ModelBuilder modelBuilder)
        => modelBuilder.HasDefaultSchema(SCHEMA)
            .ApplyConfigurationsFromAssembly(typeof(EventStoreDbContext).Assembly);
}