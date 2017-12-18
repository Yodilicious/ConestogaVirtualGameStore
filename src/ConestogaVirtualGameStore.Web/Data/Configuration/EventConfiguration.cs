namespace ConestogaVirtualGameStore.Web.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(g => g.RecordId);

            builder.Property(g => g.RecordId)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(g => g.Title)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(g => g.Description)
                .IsRequired();

            builder.Property(g => g.ImagePath)
                .IsRequired();

            builder.Property(g => g.Date)
                .HasColumnType("datetime2")
                .IsRequired();
        }
    }
}
