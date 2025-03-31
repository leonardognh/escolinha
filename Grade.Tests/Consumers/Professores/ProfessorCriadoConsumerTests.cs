using Grade.API.Consumers.Professores;
using Grade.Infrastructure.Persistence;
using MassTransit;
using MassTransit.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FluentAssertions;
using Contracts.Events.Professores;

namespace Grade.Tests.Consumers.Professores;

public class ProfessorCriadoConsumerTests
{
    [Fact]
    public async Task Deve_Consumir_ProfessorCriadoEvent_E_Salvar_Projecao()
    {
        var provider = new ServiceCollection()
            .AddDbContext<GradeDbContext>(opt =>
                opt.UseInMemoryDatabase("Test_ProfessorCriado"))
            .AddMassTransitTestHarness(cfg =>
            {
                cfg.AddConsumer<ProfessorCriadoConsumer>();
            })
            .BuildServiceProvider(true);

        var harness = provider.GetRequiredService<ITestHarness>();

        await harness.Start();

        try
        {
            var eventToSend = new ProfessorCriadoEvent(
                Id: Guid.NewGuid(),
                Nome: "Fulano de Tal",
                Email: "professor@email.com"
            );

            await harness.Bus.Publish(eventToSend);

            var consumerHarness = harness.GetConsumerHarness<ProfessorCriadoConsumer>();

            (await consumerHarness.Consumed.Any<ProfessorCriadoEvent>()).Should().BeTrue();

            var db = provider.GetRequiredService<GradeDbContext>();
            var stored = await db.ProfessoresProjecao.FindAsync(eventToSend.Id);

            stored.Should().NotBeNull();
            stored!.Nome.Should().Be("Fulano de Tal");
        }
        finally
        {
            await harness.Stop();
        }
    }
}
