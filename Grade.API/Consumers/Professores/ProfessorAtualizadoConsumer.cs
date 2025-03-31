using IntegracaoMicroservicos.Contracts.Events.Professores;
using MassTransit;

namespace Grade.API.Consumers.Professores;

public class ProfessorAtualizadoConsumer : IConsumer<ProfessorAtualizadoEvent>
{
    public Task Consume(ConsumeContext<ProfessorAtualizadoEvent> context)
    {
        var e = context.Message;

        Console.WriteLine($"🔄 [ProfessorAtualizado] {e.Id}: {e.Nome} ({e.Email})");

        // TODO: Atualizar projeção local

        return Task.CompletedTask;
    }
}
