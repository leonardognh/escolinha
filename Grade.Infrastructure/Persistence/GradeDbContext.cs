using Grade.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Grade.Infrastructure.Persistence;

public class GradeDbContext : DbContext
{
    public GradeDbContext(DbContextOptions<GradeDbContext> options)
        : base(options) { }

    public DbSet<GradeHorarios> GradeHorarioss => Set<GradeHorarios>();
    public DbSet<ProfessorProjecao> ProfessoresProjecao => Set<ProfessorProjecao>();
    public DbSet<AlunoProjecao> AlunosProjecao => Set<AlunoProjecao>();
    public DbSet<TurmaProjecao> TurmasProjecao => Set<TurmaProjecao>();
    public DbSet<MateriaProjecao> MateriasProjecao => Set<MateriaProjecao>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GradeHorarios>().ToTable("GradeHorarioss");
        modelBuilder.Entity<ProfessorProjecao>().ToTable("ProfessoresProjecao");
        modelBuilder.Entity<AlunoProjecao>().ToTable("AlunosProjecao");
        modelBuilder.Entity<TurmaProjecao>().ToTable("TurmasProjecao");
        modelBuilder.Entity<MateriaProjecao>().ToTable("MateriasProjecao");
        base.OnModelCreating(modelBuilder);
    }
}
