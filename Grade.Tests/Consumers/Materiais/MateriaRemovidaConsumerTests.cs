
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
public class MateriaRemovidaConsumerTests
{
    [Fact]
    public async Task Deve_Remover_Materia_Projecao()
    {
        var materiaId = Guid.NewGuid();

        var provider = new ServiceCollection()
            .AddDbContext<GradeDbContext>(opt => opt.UseInMemoryDatabase("MateriaRemovidaTest"))
            .AddMassTransitTestHarness(cfg => cfg.AddConsumer<MateriaRemovidaConsumer>())
            .BuildServiceProvider(true);

        var db = provider.GetRequiredService<GradeDbContext>();
        db.MateriasProjecao.Add(new MateriaProjecao { Id = materiaId, Nome = "História" });
        await db.SaveChangesAsync();

        var harness = provider.GetRequiredService<ITestHarness>();
        await harness.Start();

        try
        {
            await harness.Bus.Publish(new MateriaRemovidaEvent(materiaId));

            var materia = await db.MateriasProjecao.FindAsync(materiaId);
            materia.Should().BeNull();
        }
        finally
        {
            await harness.Stop();
        }
    }
}
