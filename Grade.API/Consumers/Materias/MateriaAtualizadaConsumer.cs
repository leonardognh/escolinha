using Grade.Infrastructure.Persistence;
using IntegracaoMicroservicos.Contracts.Events.Materias;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Grade.API.Consumers.Materias;

public class MateriaAtualizadaConsumer : IConsumer<MateriaAtualizadaEvent>
{
    private readonly GradeDbContext _db;

    public MateriaAtualizadaConsumer(GradeDbContext db)
    {
        _db = db;
    }

    public async Task Consume(ConsumeContext<MateriaAtualizadaEvent> context)
    {
        var e = context.Message;

        var materia = await _db.MateriasProjecao.FirstOrDefaultAsync(m => m.Id == e.Id);
        if (materia is null) return;

        materia.Nome = e.Nome;
        materia.Descricao = e.Descricao;
        materia.CargaHoraria = e.CargaHoraria;

        await _db.SaveChangesAsync();
    }
}
