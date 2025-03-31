using MassTransit;
using IntegracaoMicroservicos.Contracts.Events.Turmas;

namespace Grade.API.Consumers.Turmas;

public class TurmaAtualizadaConsumer : IConsumer<TurmaAtualizadaEvent>
{
    public Task Consume(ConsumeContext<TurmaAtualizadaEvent> context)
    {
        var e = context.Message;
        Console.WriteLine($"🟡 [Grade] Turma Atualizada: {e.Id} -> {e.Nome} ({e.Turno})");
        // TODO: Projeção local
        return Task.CompletedTask;
    }
}
