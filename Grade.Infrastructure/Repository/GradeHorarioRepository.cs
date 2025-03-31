using Grade.Domain.Entities;
using Grade.Domain.Interfaces;
using Grade.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Grade.Infrastructure.Repositories;

public class GradeHorarioRepository : IGradeHorarioRepository
{
    private readonly GradeDbContext _context;

    public GradeHorarioRepository(GradeDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GradeHorario>> GetAllAsync() =>
        await _context.GradeHorarios.ToListAsync();

    public async Task<(IEnumerable<GradeHorario> Data, int Total)> GetPagedAsync(int page, int pageSize)
    {
        var query = _context.GradeHorarios.AsQueryable();
        var total = await query.CountAsync();
        var data = await query
            .OrderBy(g => g.DiaSemana)
            .ThenBy(g => g.HorarioInicio)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (data, total);
    }

    public async Task<GradeHorario?> GetByIdAsync(Guid id) =>
        await _context.GradeHorarios.FindAsync(id);

    public async Task AddAsync(GradeHorario grade)
    {
        _context.GradeHorarios.Add(grade);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(GradeHorario grade)
    {
        _context.GradeHorarios.Update(grade);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var grade = await _context.GradeHorarios.FindAsync(id);
        if (grade is not null)
        {
            _context.GradeHorarios.Remove(grade);
            await _context.SaveChangesAsync();
        }
    }
}
