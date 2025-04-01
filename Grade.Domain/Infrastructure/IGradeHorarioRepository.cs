using Grade.Domain.Entities;

namespace Grade.Domain.Interfaces;

public interface IGradeHorariosRepository
{
    Task<IEnumerable<GradeHorarios>> GetAllAsync();
    Task<(IEnumerable<GradeHorarios> Data, int Total)> GetPagedAsync(int page, int pageSize);
    Task<GradeHorarios?> GetByIdAsync(Guid id);
    Task AddAsync(GradeHorarios grade);
    Task UpdateAsync(GradeHorarios grade);
    Task DeleteAsync(Guid id);
}
