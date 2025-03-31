using MassTransit;
using IntegracaoMicroservicos.Contracts.Events.Materias;

namespace Grade.API.Consumers.Materias;

public class MateriaCriadaConsumer : IConsumer<MateriaCriadaEvent>
{
    public Task Consume(ConsumeContext<MateriaCriadaEvent> context)
    {
        var e = context.Message;
        Console.WriteLine($"🟢 [Grade] Matéria Criada: {e.Nome} - Carga Horária: {e.CargaHoraria}");
        // TODO: Projeção local
        return Task.CompletedTask;
    }
}
