using Grade.Infrastructure.Persistence;
using Contracts.Events.Materias;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Grade.API.Consumers.Materias;

public class MateriaRemovidaConsumer : IConsumer<MateriaRemovidaEvent>
{
    private readonly GradeDbContext _db;

    public MateriaRemovidaConsumer(GradeDbContext db)
    {
        _db = db;
    }

    public async Task Consume(ConsumeContext<MateriaRemovidaEvent> context)
    {
        var e = context.Message;

        var materia = await _db.MateriasProjecao.FirstOrDefaultAsync(m => m.Id == e.Id);
        if (materia is not null)
        {
            _db.MateriasProjecao.Remove(materia);
            await _db.SaveChangesAsync();
        }
    }
}
