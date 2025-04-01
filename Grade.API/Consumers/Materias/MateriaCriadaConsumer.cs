using Contracts.Events.Materias;
using Grade.Domain.Entities.Projecao;
using Grade.Infrastructure.Persistence;
using MassTransit;

namespace Grade.API.Consumers.Materias;

public class MateriaCriadaConsumer : IConsumer<MateriaCriadaEvent>
{
    private readonly GradeDbContext _context;

    public MateriaCriadaConsumer(GradeDbContext context)
    {
        _context = context;
    }

    public async Task Consume(ConsumeContext<MateriaCriadaEvent> context)
    {
        var materia = new MateriaProjecao
        {
            Id = context.Message.Id,
            Nome = context.Message.Nome,
        };

        _context.MateriasProjecao.Add(materia);
        await _context.SaveChangesAsync();
    }
}