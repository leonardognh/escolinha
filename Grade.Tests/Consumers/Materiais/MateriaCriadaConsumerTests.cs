using FluentAssertions;
using Grade.API.Consumers.Materias;
using Grade.Infrastructure.Persistence;
using IntegracaoMicroservicos.Contracts.Events.Materias;
using MassTransit;
using MassTransit.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
namespace Grade.Tests.Consumers.Materiais;

public class MateriaCriadaConsumerTests
{
    [Fact]
    public async Task Deve_Salvar_Materia()
    {
        var provider = new ServiceCollection()
            .AddDbContext<GradeDbContext>(opt => opt.UseInMemoryDatabase("MateriaCriadaTest"))
            .AddMassTransitTestHarness(cfg => cfg.AddConsumer<MateriaCriadaConsumer>())
            .BuildServiceProvider(true);

        var harness = provider.GetRequiredService<ITestHarness>();
        await harness.Start();

        try
        {
            var evento = new MateriaCriadaEvent(Guid.NewGuid(), "Biologia", "Área de ciências", 60);
            await harness.Bus.Publish(evento);

            var db = provider.GetRequiredService<GradeDbContext>();
            var materia = await db.MateriasProjecao.FindAsync(evento.Id);

            materia.Should().NotBeNull();
            materia!.Nome.Should().Be("Biologia");
        }
        finally
        {
            await harness.Stop();
        }
    }
}
