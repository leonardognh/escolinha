using Grade.Domain.Entities;
using Grade.Domain.Interfaces;
using Grade.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Grade.Infrastructure.Repositories;

public class GradeHorariosRepository : IGradeHorariosRepository
{
    private readonly GradeDbContext _context;

    public GradeHorariosRepository(GradeDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GradeHorarios>> GetAllAsync() =>
        await _context.GradeHorarioss.ToListAsync();

    public async Task<(IEnumerable<GradeHorarios> Data, int Total)> GetPagedAsync(int page, int pageSize)
    {
        var query = _context.GradeHorarioss.AsQueryable();
        var total = await query.CountAsync();
        var data = await query
            .OrderBy(g => g.DiaSemana)
            .ThenBy(g => g.HorarioInicio)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (data, total);
    }

    public async Task<GradeHorarios?> GetByIdAsync(Guid id) =>
        await _context.GradeHorarioss.FindAsync(id);

    public async Task AddAsync(GradeHorarios grade)
    {
        _context.GradeHorarioss.Add(grade);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(GradeHorarios grade)
    {
        _context.GradeHorarioss.Update(grade);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var grade = await _context.GradeHorarioss.FindAsync(id);
        if (grade is not null)
        {
            _context.GradeHorarioss.Remove(grade);
            await _context.SaveChangesAsync();
        }
    }
}
