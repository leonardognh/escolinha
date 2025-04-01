using Grade.Domain.Entities;

namespace Grade.Domain.Interfaces;

public interface IGradeHorariosRepository
{
    Task<IEnumerable<GradeHorario>> GetAllAsync();
    Task<(IEnumerable<GradeHorario> Data, int Total)> GetPagedAsync(int page, int pageSize);
    Task<GradeHorario?> GetByIdAsync(Guid id);
    Task AddAsync(GradeHorario grade);
    Task UpdateAsync(GradeHorario grade);
    Task DeleteAsync(Guid id);
}
