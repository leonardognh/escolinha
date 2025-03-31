using FluentAssertions;
using Grade.API.Consumers.Turmas;
using Grade.Infrastructure.Persistence;
using IntegracaoMicroservicos.Contracts.Events.Turmas;
using MassTransit;
using MassTransit.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Grade.Tests.Consumers.Turmas;

public class TurmaCriadaConsumerTests
{
    [Fact]
    public async Task Deve_Salvar_Turma_Projecao()
    {
        var provider = new ServiceCollection()
            .AddDbContext<GradeDbContext>(opt => opt.UseInMemoryDatabase("TurmaCriadaTest"))
            .AddMassTransitTestHarness(cfg => cfg.AddConsumer<TurmaCriadaConsumer>())
            .BuildServiceProvider(true);

        var harness = provider.GetRequiredService<ITestHarness>();
        await harness.Start();

        try
        {
            var evento = new TurmaCriadaEvent(Guid.NewGuid(), "1ºB", 1, "Tarde");
            await harness.Bus.Publish(evento);

            var db = provider.GetRequiredService<GradeDbContext>();
            var turma = await db.TurmasProjecao.FindAsync(evento.Id);

            turma.Should().NotBeNull();
            turma!.Nome.Should().Be("1ºB");
            turma.Turno.Should().Be("Tarde");
        }
        finally
        {
            await harness.Stop();
        }
    }
}
