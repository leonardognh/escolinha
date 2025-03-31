using IntegracaoMicroservicos.Contracts.Events.Professores;
using MassTransit;

namespace Grade.API.Consumers.Professores;

public class ProfessorCriadoConsumer : IConsumer<ProfessorCriadoEvent>
{
    public Task Consume(ConsumeContext<ProfessorCriadoEvent> context)
    {
        var e = context.Message;

        Console.WriteLine($"📥 [ProfessorCriado] {e.Nome} ({e.Email})");

        // TODO: Salvar no banco/projeção local se necessário

        return Task.CompletedTask;
    }
}
