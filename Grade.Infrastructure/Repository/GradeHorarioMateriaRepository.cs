using Grade.Domain.Entities;
using Grade.Domain.Interfaces;
using Grade.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Grade.Infrastructure.Repositories;

public class GradeHorarioMateriaRepository : IGradeHorarioMateriaRepository
{
    private readonly GradeDbContext _context;

    public GradeHorarioMateriaRepository(GradeDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GradeHorarioMateria>> GetAllAsync() =>
        await _context.GradeHorarioMaterias.ToListAsync();

    public async Task<(IEnumerable<GradeHorarioMateria> Data, int Total)> GetPagedAsync(int page, int pageSize)
    {
        var query = _context.GradeHorarioMaterias.AsQueryable();
        var total = await query.CountAsync();
        var data = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (data, total);
    }

    public async Task<GradeHorarioMateria?> GetByIdAsync(Guid id) =>
        await _context.GradeHorarioMaterias.FindAsync(id);

    public async Task AddAsync(GradeHorarioMateria grade)
    {
        _context.GradeHorarioMaterias.Add(grade);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(GradeHorarioMateria grade)
    {
        _context.GradeHorarioMaterias.Update(grade);
        await _context.SaveChangesAsync();
    }
}
