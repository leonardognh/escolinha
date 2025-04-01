using Alunos.API.Consumers.Turmas;
using Alunos.Infrastructure.Persistence;
using FluentAssertions;
using Contracts.Events.Turmas;
using MassTransit;
using MassTransit.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Alunos.Tests.Consumers.Turmas;
public class TurmaAtualizadaConsumerTests
{
    [Fact]
    public async Task Deve_Consumir_Evento_TurmaAtualizada_Sem_Erros()
    {
        var provider = new ServiceCollection()
            .AddDbContext<AlunosDbContext>(opt => opt.UseInMemoryDatabase("TurmaAtualizadaTest"))
            .AddMassTransitTestHarness(cfg => cfg.AddConsumer<TurmaAtualizadaConsumer>())
            .BuildServiceProvider(true);

        var harness = provider.GetRequiredService<ITestHarness>();
        await harness.Start();

        try
        {
            var evento = new TurmaAtualizadaEvent(Guid.NewGuid(), "3ºB", 3);
            await harness.Bus.Publish(evento);

            var consumed = await harness.Consumed.Any<TurmaAtualizadaEvent>();
            consumed.Should().BeTrue();
        }
        finally
        {
            await harness.Stop();
        }
    }
}
