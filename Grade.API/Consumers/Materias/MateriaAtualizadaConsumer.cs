using MassTransit;
using IntegracaoMicroservicos.Contracts.Events.Materias;

namespace Grade.API.Consumers.Materias;

public class MateriaAtualizadaConsumer : IConsumer<MateriaAtualizadaEvent>
{
    public Task Consume(ConsumeContext<MateriaAtualizadaEvent> context)
    {
        var e = context.Message;
        Console.WriteLine($"🟡 [Grade] Matéria Atualizada: {e.Id} -> {e.Nome} ({e.CargaHoraria})");
        // TODO: Projeção local
        return Task.CompletedTask;
    }
}
