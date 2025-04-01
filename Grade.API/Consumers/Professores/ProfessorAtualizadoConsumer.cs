using Grade.Infrastructure.Persistence;
using Contracts.Events.Professores;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Grade.API.Consumers.Professores;

public class ProfessorAtualizadoConsumer : IConsumer<ProfessorAtualizadoEvent>
{
    private readonly GradeDbContext _db;

    public ProfessorAtualizadoConsumer(GradeDbContext db)
    {
        _db = db;
    }

    public async Task Consume(ConsumeContext<ProfessorAtualizadoEvent> context)
    {
        var e = context.Message;

        var professor = await _db.ProfessoresProjecao.FirstOrDefaultAsync(p => p.Id == e.Id);
        if (professor is null) return;

        professor.Nome = e.Nome;

        await _db.SaveChangesAsync();
    }
}
