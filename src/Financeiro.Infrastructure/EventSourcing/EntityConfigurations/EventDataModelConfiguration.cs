namespace Financeiro.Infrastructure.EventSourcing.EntityConfigurations;

public class EventDataModelConfiguration : IEntityTypeConfiguration<EventDataModel>
{
    public void Configure(EntityTypeBuilder<EventDataModel> builder)
    {
        builder.ToTable("EventStore");

        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.EventType).IsRequired().HasMaxLength(200);
        builder.Property(e => e.EventData).IsRequired();
        builder.Property(e => e.OccurredOn).IsRequired();
        builder.Property(e => e.IsPublished).IsRequired();
    }
}
