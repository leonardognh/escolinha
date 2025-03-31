using Materias.Domain.Entities;

namespace Materias.Domain.Interfaces;

public interface IMateriaRepository
{
    Task<IEnumerable<Materia>> GetAllAsync();
    Task<(IEnumerable<Materia> Data, int Total)> GetPagedAsync(int page, int pageSize);
    Task<Materia?> GetByIdAsync(Guid id);
    Task AddAsync(Materia materia);
    Task UpdateAsync(Materia materia);
    Task DeleteAsync(Guid id);
}
