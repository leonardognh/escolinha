using FluentAssertions;
using Grade.API.Consumers.Materias;
using Grade.Domain.Entities;
using Grade.Infrastructure.Persistence;
using IntegracaoMicroservicos.Contracts.Events.Materias;
using MassTransit;
using MassTransit.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Grade.Tests.Consumers.Materiais;

public class MateriaAtualizadaConsumerTests
{
    [Fact]
    public async Task Deve_Atualizar_Materia()
    {
        var id = Guid.NewGuid();

        var provider = new ServiceCollection()
            .AddDbContext<GradeDbContext>(opt => opt.UseInMemoryDatabase("MateriaAtualizadaTest"))
            .AddMassTransitTestHarness(cfg => cfg.AddConsumer<MateriaAtualizadaConsumer>())
            .BuildServiceProvider(true);

        var db = provider.GetRequiredService<GradeDbContext>();
        db.MateriasProjecao.Add(new MateriaProjecao { Id = id, Nome = "Física", CargaHoraria = 40 });
        await db.SaveChangesAsync();

        var harness = provider.GetRequiredService<ITestHarness>();
        await harness.Start();

        try
        {
            await harness.Bus.Publish(new MateriaAtualizadaEvent(id, "Física Atualizada", "Nova descrição", 80));

            var materia = await db.MateriasProjecao.FindAsync(id);
            materia!.Nome.Should().Be("Física Atualizada");
            materia.CargaHoraria.Should().Be(80);
        }
        finally
        {
            await harness.Stop();
        }
    }
}
