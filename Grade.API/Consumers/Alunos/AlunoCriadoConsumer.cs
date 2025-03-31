using IntegracaoMicroservicos.Contracts.Events.Alunos;
using MassTransit;

namespace Grade.API.Consumers.Alunos;

public class AlunoCriadoConsumer : IConsumer<AlunoCriadoEvent>
{
    public Task Consume(ConsumeContext<AlunoCriadoEvent> context)
    {
        var e = context.Message;

        Console.WriteLine($"🟢 [AlunoCriado] {e.Nome} na turma {e.TurmaId}");

        // TODO: Atualizar contagem de alunos na turma ou projeção

        return Task.CompletedTask;
    }
}
