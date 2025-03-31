using IntegracaoMicroservicos.Contracts.Events;
using MassTransit;

namespace Grade.API.Consumers;

public class ProfessorCriadoConsumer : IConsumer<ProfessorCriadoEvent>
{
    public Task Consume(ConsumeContext<ProfessorCriadoEvent> context)
    {
        var msg = context.Message;

        Console.WriteLine($"📨 Evento recebido: Professor {msg.Nome} - {msg.Email}");

        // Aqui você pode: salvar em cache, banco local, atualizar projeção, etc.

        return Task.CompletedTask;
    }
}
