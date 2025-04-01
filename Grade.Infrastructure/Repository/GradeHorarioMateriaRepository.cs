using Grade.Domain.Entities;
using Grade.Domain.Interfaces;
using Grade.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Grade.Infrastructure.Repositories;

public class GradeHorarioMateriaRepository : IGradeHorarioMateriaRepository
{
    private readonly GradeDbContext _context;
    private readonly ILogger<GradeHorarioMateriaRepository> _logger;

    public GradeHorarioMateriaRepository(GradeDbContext context, ILogger<GradeHorarioMateriaRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<GradeHorarioMateria>> GetAllAsync() =>
        await _context.GradeHorarioMaterias
            .Include(g => g.Materia)
            .Include(g => g.Professor)
            .Include(g => g.GradeHorario)
        .ToListAsync();

    public async Task<(IEnumerable<GradeHorarioMateria> Data, int Total)> GetPagedAsync(int page, int pageSize)
    {
        var query = _context.GradeHorarioMaterias.AsQueryable();
        var total = await query.CountAsync();
        var data = await query
            .Include(g => g.Materia)
            .Include(g => g.Professor)
            .Include(g => g.GradeHorario)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (data, total);
    }

    public async Task<GradeHorarioMateria?> GetByIdAsync(Guid gradeHorarioId)
    {
        return await _context.GradeHorarioMaterias
            .Include(g => g.Materia)
            .Include(g => g.Professor)
            .Include(g => g.GradeHorario)
            .FirstOrDefaultAsync(g =>
                g.GradeHorarioId == gradeHorarioId);
    }

    public async Task AddAsync(GradeHorarioMateria grade)
    {
        try
        {
            _logger.LogInformation($"AddAsync: {JsonSerializer.Serialize(grade)}");
            _context.GradeHorarioMaterias.Add(grade);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Exception: {JsonSerializer.Serialize(ex)}");
        }
    }

    public async Task UpdateAsync(GradeHorarioMateria grade)
    {
        _context.GradeHorarioMaterias.Update(grade);
        await _context.SaveChangesAsync();
    }
}
