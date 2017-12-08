namespace ConestogaVirtualGameStore.Web.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class ShoppingCartConfiguration : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> builder)
        {
            builder.HasKey(s => s.RecordId);

            builder.Property(s => s.RecordId)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(s => s.User)
                .IsRequired();

            builder.Property(s => s.PurcheasedOn)
                .HasColumnType("datetime2")
                .IsRequired();

            builder.Property(s => s.HasPaid)
                .IsRequired();

            builder.HasMany(s => s.ShoppingCartItems)
                .WithOne(s => s.ShoppingCart)
                .HasForeignKey(s => s.ShoppingCartId);
        }
    }
}
