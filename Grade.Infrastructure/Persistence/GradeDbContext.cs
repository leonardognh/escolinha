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
            .HasKey(x => new { x.Id, x.ProfessorId, x.MateriaId });


        modelBuilder.Entity<GradeHorarioMateria>()
            .HasOne(x => x.GradeHorario)
            .WithMany(x => x.Materias)
            .HasForeignKey(x => x.Id);

        modelBuilder.Entity<ProfessorProjecao>().ToTable("ProfessoresProjecao");
        modelBuilder.Entity<AlunoProjecao>().ToTable("AlunosProjecao");
        modelBuilder.Entity<TurmaProjecao>().ToTable("TurmasProjecao");
        modelBuilder.Entity<MateriaProjecao>().ToTable("MateriasProjecao");
        base.OnModelCreating(modelBuilder);
    }
}
