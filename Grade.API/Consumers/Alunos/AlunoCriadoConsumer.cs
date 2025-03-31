using Grade.Domain.Entities;
using Grade.Infrastructure.Persistence;
using IntegracaoMicroservicos.Contracts.Events.Alunos;
using MassTransit;

namespace Grade.API.Consumers.Alunos;

public class AlunoCriadoConsumer : IConsumer<AlunoCriadoEvent>
{
    private readonly GradeDbContext _db;

    public AlunoCriadoConsumer(GradeDbContext db)
    {
        _db = db;
    }

    public async Task Consume(ConsumeContext<AlunoCriadoEvent> context)
    {
        var e = context.Message;

        var exists = await _db.AlunosProjecao.FindAsync(e.Id);
        if (exists is not null) return;

        var aluno = new AlunoProjecao
        {
            Id = e.Id,
            Nome = e.Nome,
            TurmaId = e.TurmaId
        };

        _db.AlunosProjecao.Add(aluno);
        await _db.SaveChangesAsync();
    }
}
