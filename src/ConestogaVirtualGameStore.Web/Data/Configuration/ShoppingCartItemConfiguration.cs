namespace ConestogaVirtualGameStore.Web.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class ShoppingCartItemConfiguration : IEntityTypeConfiguration<ShoppingCartItem>
    {
        public void Configure(EntityTypeBuilder<ShoppingCartItem> builder)
        {
            builder.HasKey(s => s.RecordId);

            builder.Property(s => s.RecordId)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(s => s.Price)
                .IsRequired();

            builder.Property(s => s.AddedOn)
                .HasColumnType("datetime2")
                .IsRequired();
        }
    }
}
