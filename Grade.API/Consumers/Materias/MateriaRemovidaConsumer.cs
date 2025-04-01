using Contracts.Events.Materias;
using Grade.Infrastructure.Persistence;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Grade.API.Consumers.Materias;

public class MateriaRemovidaConsumer : IConsumer<MateriaRemovidaEvent>
{
    private readonly GradeDbContext _context;

    public MateriaRemovidaConsumer(GradeDbContext context)
    {
        _context = context;
    }

    public async Task Consume(ConsumeContext<MateriaRemovidaEvent> context)
    {
        var materia = await _context.MateriasProjecao
            .FirstOrDefaultAsync(m => m.Id == context.Message.Id);

        if (materia is not null)
        {
            _context.MateriasProjecao.Remove(materia);
            await _context.SaveChangesAsync();
        }
    }
}