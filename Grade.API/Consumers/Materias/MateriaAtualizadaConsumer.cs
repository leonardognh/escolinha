using Contracts.Events.Materias;
using Grade.Infrastructure.Persistence;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Grade.API.Consumers.Materias;

public class MateriaAtualizadaConsumer : IConsumer<MateriaAtualizadaEvent>
{
    private readonly GradeDbContext _context;

    public MateriaAtualizadaConsumer(GradeDbContext context)
    {
        _context = context;
    }

    public async Task Consume(ConsumeContext<MateriaAtualizadaEvent> context)
    {
        var materia = await _context.MateriasProjecao
            .FirstOrDefaultAsync(m => m.Id == context.Message.Id);

        if (materia is not null)
        {
            materia.Nome = context.Message.Nome;
            await _context.SaveChangesAsync();
        }
    }
}