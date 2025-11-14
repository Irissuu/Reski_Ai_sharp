using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reski.Domain.Entity;

namespace Reski.Infrastructure.Mapping;

public class TrilhaMapping : IEntityTypeConfiguration<Trilha>
{
    public void Configure(EntityTypeBuilder<Trilha> builder)
    {
        builder.ToTable("TrilhaCsharp");

        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).ValueGeneratedOnAdd();

        builder.Property(t => t.Status)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(t => t.Conteudo)
            .IsRequired()
            .HasMaxLength(4000);

        builder.Property(t => t.Competencia)
            .IsRequired()
            .HasMaxLength(255);
    }
}