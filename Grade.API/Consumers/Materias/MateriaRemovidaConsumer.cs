using MassTransit;
using IntegracaoMicroservicos.Contracts.Events.Materias;

namespace Grade.API.Consumers.Materias;

public class MateriaRemovidaConsumer : IConsumer<MateriaRemovidaEvent>
{
    public Task Consume(ConsumeContext<MateriaRemovidaEvent> context)
    {
        var e = context.Message;
        Console.WriteLine($"❌ [Grade] Matéria Removida: {e.Id}");
        // TODO: Remover projeção local
        return Task.CompletedTask;
    }
}
