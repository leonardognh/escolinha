using Grade.Domain.Entities;

namespace Grade.Domain.Interfaces;

public interface IGradeHorarioMateriaRepository
{
    Task<IEnumerable<GradeHorarioMateria>> GetAllAsync();
    Task<(IEnumerable<GradeHorarioMateria> Data, int Total)> GetPagedAsync(int page, int pageSize);
    Task<GradeHorarioMateria?> GetByIdAsync(Guid gradeHorarioId);
    Task AddAsync(GradeHorarioMateria gradeHorarioMateria);
    Task UpdateAsync(GradeHorarioMateria gradeHorarioMateria);
}
