using Alunos.Domain.Entities;

namespace Alunos.Domain.Interfaces;

public interface IAlunoRepository
{
    Task<IEnumerable<Aluno>> GetAllAsync();
    Task<(IEnumerable<Aluno> Data, int Total)> GetPagedAsync(int page, int pageSize);
    Task<Aluno?> GetByIdAsync(Guid id);
    Task AddAsync(Aluno aluno);
    Task UpdateAsync(Aluno aluno);
    Task DeleteAsync(Guid id);
}
