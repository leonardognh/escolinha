using Alunos.Domain.Entities;
using Alunos.Domain.Interfaces;
using Alunos.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Alunos.Infrastructure.Repositories;

public class AlunoRepository : IAlunoRepository
{
    private readonly AlunosDbContext _context;

    public AlunoRepository(AlunosDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Aluno>> GetAllAsync() =>
        await _context.Alunos.ToListAsync();

    public async Task<(IEnumerable<Aluno> Data, int Total)> GetPagedAsync(int page, int pageSize)
    {
        var query = _context.Alunos.AsQueryable();

        var total = await query.CountAsync();
        var data = await query
            .OrderBy(a => a.Nome)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (data, total);
    }

    public async Task<Aluno?> GetByIdAsync(Guid id) =>
        await _context.Alunos.FindAsync(id);

    public async Task AddAsync(Aluno aluno)
    {
        _context.Alunos.Add(aluno);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Aluno aluno)
    {
        _context.Alunos.Update(aluno);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var aluno = await _context.Alunos.FindAsync(id);
        if (aluno is not null)
        {
            _context.Alunos.Remove(aluno);
            await _context.SaveChangesAsync();
        }
    }
}
