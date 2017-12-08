using ConestogaVirtualGameStore.Web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConestogaVirtualGameStore.Web.Data
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
