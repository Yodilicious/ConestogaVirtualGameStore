namespace ConestogaVirtualGameStore.Web.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(r => r.RecordId);

            builder.Property(r => r.RecordId)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(r => r.Author)
                .IsRequired();

            builder.Property(r => r.Title)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(r => r.ReviewText)
                .IsRequired();

            builder.Property(r => r.Date)
                .HasColumnType("datetime2")
                .IsRequired();
        }
    }
}
