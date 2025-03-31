using IntegracaoMicroservicos.Contracts.Events.Alunos;
using MassTransit;

namespace Grade.API.Consumers.Alunos;

public class AlunoTransferidoConsumer : IConsumer<AlunoTransferidoEvent>
{
    public Task Consume(ConsumeContext<AlunoTransferidoEvent> context)
    {
        var e = context.Message;

        Console.WriteLine($"🔄 [AlunoTransferido] {e.AlunoId} de {e.TurmaAnteriorId} para {e.TurmaNovaId}");

        // TODO: Atualizar projeção de alunos por turma

        return Task.CompletedTask;
    }
}
