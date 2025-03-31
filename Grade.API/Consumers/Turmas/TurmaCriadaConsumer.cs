using MassTransit;
using IntegracaoMicroservicos.Contracts.Events.Turmas;

namespace Grade.API.Consumers.Turmas;

public class TurmaCriadaConsumer : IConsumer<TurmaCriadaEvent>
{
    public Task Consume(ConsumeContext<TurmaCriadaEvent> context)
    {
        var e = context.Message;
        Console.WriteLine($"🟢 [Grade] Nova Turma Criada: {e.Nome} ({e.Turno}) - Ano {e.Ano}");
        // TODO: Projeção local
        return Task.CompletedTask;
    }
}
