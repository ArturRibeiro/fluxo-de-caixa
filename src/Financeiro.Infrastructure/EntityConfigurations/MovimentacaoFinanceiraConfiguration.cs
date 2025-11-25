namespace Financeiro.Infrastructure.EntityConfigurations;

public class MovimentacaoFinanceiraConfiguration : IEntityTypeConfiguration<MovimentacaoFinanceira>
{
    public void Configure(EntityTypeBuilder<MovimentacaoFinanceira> builder)
    {
        builder.ToTable("MovimentacoesFinanceiras");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Data).HasColumnType("timestamp").IsRequired();
        builder.Property(x => x.Valor).HasColumnType("decimal(18,2)").IsRequired();
        
        builder.OwnsOne(x => x.Tipo, tipo =>
        {
            tipo.Property(x => x.Codigo).HasColumnName("tipo_movimentacao_codigo").IsRequired();
            tipo.Property(x => x.Nome).HasColumnName("tipo_movimentacao_nome").HasMaxLength(40).IsRequired();
        });
        
        builder.Property(x => x.Descricao).HasMaxLength(500).IsRequired(false);
    }
}