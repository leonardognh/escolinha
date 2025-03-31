using Grade.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Grade.Infrastructure.Persistence;

public class GradeDbContext : DbContext
{
    public GradeDbContext(DbContextOptions<GradeDbContext> options)
        : base(options) { }

    public DbSet<GradeHorario> GradeHorarios => Set<GradeHorario>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GradeHorario>().ToTable("GradeHorarios");
        base.OnModelCreating(modelBuilder);
    }
}
