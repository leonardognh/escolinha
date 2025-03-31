using Grade.Domain.Entities;
using Grade.Infrastructure.Persistence;
using Contracts.Events.Materias;
using MassTransit;

namespace Grade.API.Consumers.Materias;

public class MateriaCriadaConsumer : IConsumer<MateriaCriadaEvent>
{
    private readonly GradeDbContext _db;

    public MateriaCriadaConsumer(GradeDbContext db)
    {
        _db = db;
    }

    public async Task Consume(ConsumeContext<MateriaCriadaEvent> context)
    {
        var e = context.Message;

        var exists = await _db.MateriasProjecao.FindAsync(e.Id);
        if (exists is not null) return;

        var materia = new MateriaProjecao
        {
            Id = e.Id,
            Nome = e.Nome,
            Descricao = e.Descricao,
            CargaHoraria = e.CargaHoraria
        };

        _db.MateriasProjecao.Add(materia);
        await _db.SaveChangesAsync();
    }
}
