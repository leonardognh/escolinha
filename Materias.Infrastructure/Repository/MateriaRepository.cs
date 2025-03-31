using Microsoft.EntityFrameworkCore;
using Materias.Domain.Entities;
using Materias.Domain.Interfaces;
using Materias.Infrastructure.Persistence;

namespace Materias.Infrastructure.Repositories;

public class MateriaRepository : IMateriaRepository
{
    private readonly MateriasDbContext _context;

    public MateriaRepository(MateriasDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Materia>> GetAllAsync() =>
        await _context.Materias.ToListAsync();

    public async Task<(IEnumerable<Materia> Data, int Total)> GetPagedAsync(int page, int pageSize)
    {
        var query = _context.Materias.AsQueryable();
        var total = await query.CountAsync();
        var data = await query
            .OrderBy(m => m.Nome)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (data, total);
    }

    public async Task<Materia?> GetByIdAsync(Guid id) =>
        await _context.Materias.FindAsync(id);

    public async Task AddAsync(Materia materia)
    {
        _context.Materias.Add(materia);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Materia materia)
    {
        _context.Materias.Update(materia);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var materia = await _context.Materias.FindAsync(id);
        if (materia is not null)
        {
            _context.Materias.Remove(materia);
            await _context.SaveChangesAsync();
        }
    }
}
