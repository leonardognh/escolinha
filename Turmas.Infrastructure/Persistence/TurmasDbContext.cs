using Microsoft.EntityFrameworkCore;
using Turmas.Domain.Entities;

namespace Turmas.Infrastructure.Persistence;

public class TurmasDbContext : DbContext
{
    public TurmasDbContext(DbContextOptions<TurmasDbContext> options) : base(options) { }

    public DbSet<Turma> Turmas => Set<Turma>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Turma>().ToTable("Turmas");
        base.OnModelCreating(modelBuilder);
    }
}
