using FluentAssertions;
using Grade.API.Consumers.Materias;
using Grade.Infrastructure.Persistence;
using Contracts.Events.Materias;
using MassTransit;
using MassTransit.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Grade.Domain.Entities.Projecao;

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
        db.MateriasProjecao.Add(new MateriaProjecao { Id = id, Nome = "Física" });
        await db.SaveChangesAsync();

        var harness = provider.GetRequiredService<ITestHarness>();
        await harness.Start();

        try
        {
            await harness.Bus.Publish(new MateriaAtualizadaEvent(id, "Física Atualizada"));

            var materia = await db.MateriasProjecao.FindAsync(id);
            materia!.Nome.Should().Be("Física Atualizada");
        }
        finally
        {
            await harness.Stop();
        }
    }
}
