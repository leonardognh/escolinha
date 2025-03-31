using Microsoft.EntityFrameworkCore;
using Professores.Domain.Entities;

namespace Professores.Infrastructure.Persistence;

public class ProfessoresDbContext : DbContext
{
    public ProfessoresDbContext(DbContextOptions<ProfessoresDbContext> options)
        : base(options) { }

    public DbSet<Professor> Professores => Set<Professor>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Professor>().ToTable("Professores");
        base.OnModelCreating(modelBuilder);
    }
}
