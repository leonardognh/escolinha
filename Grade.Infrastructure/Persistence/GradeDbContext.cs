using Grade.Domain.Entities;
using Grade.Domain.Entities.Projecao;
using Microsoft.EntityFrameworkCore;

namespace Grade.Infrastructure.Persistence;

public class GradeDbContext : DbContext
{
    public GradeDbContext(DbContextOptions<GradeDbContext> options)
        : base(options) { }

    public DbSet<GradeHorario> GradeHorarios => Set<GradeHorario>();
    public DbSet<GradeHorarioMateria> GradeHorarioMaterias => Set<GradeHorarioMateria>();
    public DbSet<ProfessorProjecao> ProfessoresProjecao => Set<ProfessorProjecao>();
    public DbSet<AlunoProjecao> AlunosProjecao => Set<AlunoProjecao>();
    public DbSet<TurmaProjecao> TurmasProjecao => Set<TurmaProjecao>();
    public DbSet<MateriaProjecao> MateriasProjecao => Set<MateriaProjecao>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GradeHorario>().ToTable("GradeHorarios");

        modelBuilder.Entity<GradeHorarioMateria>().ToTable("GradeHorarioMaterias")
            .HasKey(x => new { x.GradeHorarioId, x.ProfessorId, x.MateriaId });

        modelBuilder.Entity<GradeHorarioMateria>()
            .HasOne(g => g.GradeHorario)
            .WithMany() 
            .HasForeignKey(g => g.GradeHorarioId);

        modelBuilder.Entity<GradeHorarioMateria>()
            .HasOne(g => g.Materia)
            .WithMany()
            .HasForeignKey(g => g.MateriaId);

        modelBuilder.Entity<GradeHorarioMateria>()
            .HasOne(g => g.Professor)
            .WithMany() 
            .HasForeignKey(g => g.ProfessorId);

        modelBuilder.Entity<ProfessorProjecao>().ToTable("ProfessoresProjecao");
        modelBuilder.Entity<AlunoProjecao>().ToTable("AlunosProjecao");
        modelBuilder.Entity<TurmaProjecao>().ToTable("TurmasProjecao");
        modelBuilder.Entity<MateriaProjecao>().ToTable("MateriasProjecao");
        base.OnModelCreating(modelBuilder);
    }
}
