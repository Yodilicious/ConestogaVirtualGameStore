namespace ConestogaVirtualGameStore.Web.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class EventRegistrationConfiguration : IEntityTypeConfiguration<EventRegistration>
    {
        public void Configure(EntityTypeBuilder<EventRegistration> builder)
        {
            builder.HasKey(g => g.RecordId);

            builder.Property(g => g.RecordId)
                .ValueGeneratedOnAdd()
                .IsRequired();
            
            builder.Property(g => g.RegisteredOn)
                .HasColumnType("datetime2")
                .IsRequired();
        }
    }
}
