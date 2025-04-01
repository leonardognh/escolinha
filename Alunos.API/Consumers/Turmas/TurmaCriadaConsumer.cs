using MassTransit;
using Contracts.Events.Turmas;

namespace Alunos.API.Consumers.Turmas;

public class TurmaCriadaConsumer : IConsumer<TurmaCriadaEvent>
{
    public Task Consume(ConsumeContext<TurmaCriadaEvent> context)
    {
        var e = context.Message;
        Console.WriteLine($"🟢 [Alunos] Nova Turma Criada: {e.Nome}");
        return Task.CompletedTask;
    }
}
