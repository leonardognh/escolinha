using Microsoft.EntityFrameworkCore;
using Materias.Domain.Entities;

namespace Materias.Infrastructure.Persistence;

public class MateriasDbContext : DbContext
{
    public MateriasDbContext(DbContextOptions<MateriasDbContext> options) : base(options) { }

    public DbSet<Materia> Materias => Set<Materia>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Materia>().ToTable("Materias");
        base.OnModelCreating(modelBuilder);
    }
}
