namespace Financeiro.Consolidado.Command.Queries;

public interface IApplicationDbContextReading
{
    IQueryable<TEntity> DbSet<TEntity>() where TEntity : class;
}