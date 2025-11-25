namespace Financeiro.Consolidado.Infrastructure.EntityConfigurations;

public class ConsolidadoDiarioMap : IEntityTypeConfiguration<Domain.Periodo>
{
    public void Configure(EntityTypeBuilder<Domain.Periodo> builder)
    {
        builder.ToTable("Periodos");
        builder.HasKey(x => x.Id);

        builder.OwnsMany(x => x.Movimentacoes, mov =>
        {
            mov.ToTable("Movimentacoes");
            mov.WithOwner().HasForeignKey("consolidado_id");
            mov.Property<long>("Id");
            mov.HasKey("Id");

            mov.Property(x => x.Descricao).HasMaxLength(200).IsRequired();

            mov.OwnsOne(x => x.Tipo, t =>
            {
                t.Property(x => x.Value).HasColumnName("tipo_movimentacao_codigo");//.IsRequired();
                t.Property(x => x.Descricao).HasColumnName("tipo_movimentacao_nome");//.HasMaxLength(40).IsRequired();
            });
        });

        builder.Property(x => x.TotalEntradas).HasColumnName("total_entradas").HasDefaultValue(0).IsRequired();
        builder.Property(x => x.TotalSaidas).HasColumnName("total_saidas").HasDefaultValue(0).IsRequired();
        builder.Property(x => x.Saldo).HasColumnName("saldo").HasDefaultValue(0).IsRequired();
        builder.Property(x => x.Data).HasColumnType("date").IsRequired();
    }
}
