// namespace Financeiro.Consolidado.Infrastructure.EntityConfigurations;
//
// public class MovimentacaoDiariaMap: IEntityTypeConfiguration<MovimentacaoDiaria>
// {
//     public void Configure(EntityTypeBuilder<MovimentacaoDiaria> builder)
//     {
//         builder.ToTable("Movimentacoes");
//         builder.HasKey(x => x.Id);
//         builder.Property(x => x.Id).ValueGeneratedOnAdd();
//         builder.Property(x => x.Descricao).HasMaxLength(200).IsRequired();
//
//         builder.HasOne(x => x.Periodo)
//             .WithMany(x => x.Movimentacoes)
//             .HasForeignKey(x => x.PeriodoId)
//             .OnDelete(DeleteBehavior.Restrict);
//         
//         builder.OwnsOne(x => x.Tipo, t =>
//         {
//             t.Property(x => x.Value).HasColumnName("tipo_movimentacao_codigo").IsRequired();
//             t.Property(x => x.Descricao).HasColumnName("tipo_movimentacao_nome").HasMaxLength(40).IsRequired();
//         });
//     }
// }