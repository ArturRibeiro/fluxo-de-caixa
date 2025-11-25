namespace Financeiro.Infrastructure;

public partial class ApplicationDbContext : DbContext, IApplicationDbContext 
{
    private readonly IMediator _mediator;
    private const string SCHEMA = "lancamentos";
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IMediator mediator) : base(options)
    {
        _mediator = mediator;
        // // A partir da versão 6.0, o Npgsql passou a mapear:
        // // DateTime em UTC → para timestamp with time zone (timestamptz)
        // // DateTime Local ou Unspecified → para timestamp without time zone
        // //     Enviar um DateTime que não esteja em UTC para um campo timestamptz gera exceção.
        // //     DateTime também é suportado para timestamptz, mas apenas com offset zero (UTC).
        // //     Antes da versão 6.0, o timestamptz era convertido para horário local ao ser lido.
        // //     Há melhorias e mudanças incompatíveis detalhadas nos breaking changes da versão 6.0.
        // //https://www.npgsql.org/doc/types/datetime.html
        // AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    
    public DbSet<MovimentacaoFinanceira> MovimentacaoFinanceiras { get; set; }

    protected override void OnModelCreating
        (ModelBuilder modelBuilder)
        => modelBuilder.HasDefaultSchema(SCHEMA)
            .ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var result = await base.SaveChangesAsync(cancellationToken);
        await DispatchDomainEventsAsync();
        return result;
    }

    private async Task DispatchDomainEventsAsync()
    {
        var domainEvents = ChangeTracker.Entries<AggregateRoot>()
            .SelectMany(x => x.Entity.Events)
            .ToList();

        foreach (var evt in domainEvents)
            await _mediator.Publish(evt);

        foreach (var entity in ChangeTracker.Entries<AggregateRoot>())
            entity.Entity.ClearEvents();
    }
}