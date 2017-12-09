namespace ConestogaVirtualGameStore.Web.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using ConestogaVirtualGameStore.Web.Models;

    public class WishlistConfiguration : IEntityTypeConfiguration<Wishlist>
    {
        public void Configure(EntityTypeBuilder<Wishlist> builder)
        {
            builder.HasKey(s => s.RecordId);

            builder.Property(s => s.RecordId)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(s => s.User)
                .IsRequired();
        }
    }
}
