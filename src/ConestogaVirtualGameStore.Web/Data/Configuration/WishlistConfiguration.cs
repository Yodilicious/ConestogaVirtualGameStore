using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ConestogaVirtualGameStore.Web.Models;


namespace ConestogaVirtualGameStore.Web.Data.Configuration
{
    public class WishlistConfiguration : IEntityTypeConfiguration<Wishlist>
    {
        public void Configure(EntityTypeBuilder<Wishlist> builder)
        {
            builder.HasKey(s => s.RecordId);

            builder.Property(s => s.RecordId)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(s => s.Title)
                .IsRequired();

            builder.Property(s => s.ImageFileName)
                .IsRequired();

            builder.Property(s => s.Price)
                .IsRequired();
        }
    }
}
