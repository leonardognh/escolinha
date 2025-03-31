using Grade.Domain.Entities;
using Grade.Infrastructure.Persistence;
using Contracts.Events.Turmas;
using MassTransit;

namespace Grade.API.Consumers.Turmas;

public class TurmaCriadaConsumer : IConsumer<TurmaCriadaEvent>
{
    private readonly GradeDbContext _db;

    public TurmaCriadaConsumer(GradeDbContext db)
    {
        _db = db;
    }

    public async Task Consume(ConsumeContext<TurmaCriadaEvent> context)
    {
        var e = context.Message;

        var exists = await _db.TurmasProjecao.FindAsync(e.Id);
        if (exists is not null) return;

        var turma = new TurmaProjecao
        {
            Id = e.Id,
            Nome = e.Nome,
            Ano = e.Ano,
            Turno = e.Turno
        };

        _db.TurmasProjecao.Add(turma);
        await _db.SaveChangesAsync();
    }
}
