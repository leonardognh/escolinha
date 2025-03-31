using Professores.Domain.Entities;

namespace Professores.Domain.Interfaces;

public interface IProfessorRepository
{
    Task<(IEnumerable<Professor> Data, int Total)> GetPagedAsync(int page, int pageSize);
    Task<Professor?> GetByIdAsync(Guid id);
    Task AddAsync(Professor professor);
    Task UpdateAsync(Professor professor);
    Task DeleteAsync(Guid id);
}
