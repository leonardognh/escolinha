using Alunos.API.Consumers.Turmas;
using Alunos.Infrastructure.Persistence;
using FluentAssertions;
using IntegracaoMicroservicos.Contracts.Events.Turmas;
using MassTransit;
using MassTransit.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Alunos.Tests.Consumers.Turmas;
public class TurmaCriadaConsumerTests
{
    [Fact]
    public async Task Deve_Consumir_Evento_TurmaCriada_Sem_Erros()
    {
        var provider = new ServiceCollection()
            .AddDbContext<AlunosDbContext>(opt => opt.UseInMemoryDatabase("TurmaCriadaTest"))
            .AddMassTransitTestHarness(cfg => { cfg.AddConsumer<TurmaCriadaConsumer>(); })
            .BuildServiceProvider(true);

        var harness = provider.GetRequiredService<ITestHarness>();
        await harness.Start();

        try
        {
            var evento = new TurmaCriadaEvent(Guid.NewGuid(), "3ºA", 3, "Manhã");
            await harness.Bus.Publish(evento);

            var consumed = await harness.Consumed.Any<TurmaCriadaEvent>();
            consumed.Should().BeTrue();
        }
        finally
        {
            await harness.Stop();
        }
    }
}
