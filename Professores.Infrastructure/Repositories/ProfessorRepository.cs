using Microsoft.EntityFrameworkCore;
using Professores.Domain.Entities;
using Professores.Domain.Interfaces;
using Professores.Infrastructure.Persistence;

namespace Professores.Infrastructure.Repositories;

public class ProfessorRepository : IProfessorRepository
{
    private readonly ProfessoresDbContext _context;

    public ProfessorRepository(ProfessoresDbContext context)
    {
        _context = context;
    }

    public async Task<(IEnumerable<Professor> Data, int Total)> GetPagedAsync(int page, int pageSize)
    {
        var query = _context.Professores.AsQueryable();

        var total = await query.CountAsync();
        var data = await query
            .OrderBy(p => p.Nome)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (data, total);
    }

    public async Task<Professor?> GetByIdAsync(Guid id) =>
        await _context.Professores.FindAsync(id);

    public async Task AddAsync(Professor professor)
    {
        _context.Professores.Add(professor);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Professor professor)
    {
        _context.Professores.Update(professor);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var professor = await _context.Professores.FindAsync(id);
        if (professor != null)
        {
            _context.Professores.Remove(professor);
            await _context.SaveChangesAsync();
        }
    }
}
