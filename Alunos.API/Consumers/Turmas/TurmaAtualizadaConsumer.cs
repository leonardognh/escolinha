using MassTransit;
using Contracts.Events.Turmas;

namespace Alunos.API.Consumers.Turmas;

public class TurmaAtualizadaConsumer : IConsumer<TurmaAtualizadaEvent>
{
    public Task Consume(ConsumeContext<TurmaAtualizadaEvent> context)
    {
        var e = context.Message;
        Console.WriteLine($"🟡 [Alunos] Turma Atualizada: {e.Id} -> {e.Nome} ({e.Turno})");
        return Task.CompletedTask;
    }
}
