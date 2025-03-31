using Turmas.Domain.Entities;

namespace Turmas.Domain.Interfaces;

public interface ITurmaRepository
{
    Task<IEnumerable<Turma>> GetAllAsync();
    Task<(IEnumerable<Turma> Data, int Total)> GetPagedAsync(int page, int pageSize);
    Task<Turma?> GetByIdAsync(Guid id);
    Task AddAsync(Turma turma);
    Task UpdateAsync(Turma turma);
    Task DeleteAsync(Guid id);
}
