using Alunos.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Alunos.Infrastructure.Persistence;

public class AlunosDbContext : DbContext
{
    public AlunosDbContext(DbContextOptions<AlunosDbContext> options)
        : base(options) { }

    public DbSet<Aluno> Alunos => Set<Aluno>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aluno>().ToTable("Alunos");
        base.OnModelCreating(modelBuilder);
    }
}
