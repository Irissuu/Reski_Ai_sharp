using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reski.Domain.Entity;

namespace Reski.Infrastructure.Mapping;

public class ObjetivoMapping : IEntityTypeConfiguration<Objetivo>
{
    public void Configure(EntityTypeBuilder<Objetivo> builder)
    {
        builder.ToTable("ObjetivoCsharp");

        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id).ValueGeneratedOnAdd();

        builder.Property(o => o.Cargo)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(o => o.Area)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(o => o.Descricao)
            .IsRequired()
            .HasMaxLength(4000);

        builder.Property(o => o.Demanda)
            .IsRequired()
            .HasMaxLength(255);
    }
}