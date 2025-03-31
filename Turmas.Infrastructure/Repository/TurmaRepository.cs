using Microsoft.EntityFrameworkCore;
using Turmas.Domain.Entities;
using Turmas.Domain.Interfaces;
using Turmas.Infrastructure.Persistence;

namespace Turmas.Infrastructure.Repositories;

public class TurmaRepository : ITurmaRepository
{
    private readonly TurmasDbContext _context;

    public TurmaRepository(TurmasDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Turma>> GetAllAsync() =>
        await _context.Turmas.ToListAsync();

    public async Task<(IEnumerable<Turma> Data, int Total)> GetPagedAsync(int page, int pageSize)
    {
        var query = _context.Turmas.AsQueryable();
        var total = await query.CountAsync();
        var data = await query
            .OrderBy(t => t.Nome)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (data, total);
    }

    public async Task<Turma?> GetByIdAsync(Guid id) =>
        await _context.Turmas.FindAsync(id);

    public async Task AddAsync(Turma turma)
    {
        _context.Turmas.Add(turma);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Turma turma)
    {
        _context.Turmas.Update(turma);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var turma = await _context.Turmas.FindAsync(id);
        if (turma is not null)
        {
            _context.Turmas.Remove(turma);
            await _context.SaveChangesAsync();
        }
    }
}
