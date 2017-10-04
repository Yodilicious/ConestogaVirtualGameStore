﻿namespace ConestogaVirtualGameStore.Presentation.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
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

            builder.Property(g => g.Developer)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(g => g.Publisher)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(g => g.Price)
                .IsRequired();

            builder.Property(g => g.Date)
                .HasColumnType("datetime2")
                .IsRequired();

            builder.HasMany(g => g.Reviews)
                .WithOne(r => r.Game)
                .HasForeignKey(g => g.GameId);
        }
    }
}
