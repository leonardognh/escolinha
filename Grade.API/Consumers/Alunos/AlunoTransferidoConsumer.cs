using Grade.Infrastructure.Persistence;
using Contracts.Events.Alunos;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Grade.API.Consumers.Alunos;

public class AlunoTransferidoConsumer : IConsumer<AlunoTransferidoEvent>
{
    private readonly GradeDbContext _db;

    public AlunoTransferidoConsumer(GradeDbContext db)
    {
        _db = db;
    }

    public async Task Consume(ConsumeContext<AlunoTransferidoEvent> context)
    {
        var e = context.Message;

        var aluno = await _db.AlunosProjecao.FirstOrDefaultAsync(a => a.Id == e.AlunoId);
        if (aluno is null) return;

        aluno.TurmaId = e.TurmaNovaId;

        await _db.SaveChangesAsync();
    }
}
