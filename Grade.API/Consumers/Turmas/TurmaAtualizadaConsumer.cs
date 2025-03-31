using Grade.Infrastructure.Persistence;
using IntegracaoMicroservicos.Contracts.Events.Turmas;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Grade.API.Consumers.Turmas;

public class TurmaAtualizadaConsumer : IConsumer<TurmaAtualizadaEvent>
{
    private readonly GradeDbContext _db;

    public TurmaAtualizadaConsumer(GradeDbContext db)
    {
        _db = db;
    }

    public async Task Consume(ConsumeContext<TurmaAtualizadaEvent> context)
    {
        var e = context.Message;

        var turma = await _db.TurmasProjecao.FirstOrDefaultAsync(t => t.Id == e.Id);
        if (turma is null) return;

        turma.Nome = e.Nome;
        turma.Ano = e.Ano;
        turma.Turno = e.Turno;

        await _db.SaveChangesAsync();
    }
}
