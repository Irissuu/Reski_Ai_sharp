using Microsoft.EntityFrameworkCore;
using Reski.Domain.Entity;
using Reski.Infrastructure.Mapping;

namespace Reski.Infrastructure.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Usuario>  Usuarios  { get; set; } = default!;
    public DbSet<Trilha>   Trilhas   { get; set; } = default!;
    public DbSet<Objetivo> Objetivos { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UsuarioMapping());
        modelBuilder.ApplyConfiguration(new TrilhaMapping());
        modelBuilder.ApplyConfiguration(new ObjetivoMapping());

        base.OnModelCreating(modelBuilder);
    }
}