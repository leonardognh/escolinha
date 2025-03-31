using FluentAssertions;
using Grade.API.Consumers.Turmas;
using Grade.Domain.Entities;
using Grade.Infrastructure.Persistence;
using IntegracaoMicroservicos.Contracts.Events.Turmas;
using MassTransit;
using MassTransit.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Grade.Tests.Consumers.Turmas;

public class TurmaAtualizadaConsumerTests
{
    [Fact]
    public async Task Deve_Atualizar_Turma()
    {
        var id = Guid.NewGuid();

        var provider = new ServiceCollection()
            .AddDbContext<GradeDbContext>(opt => opt.UseInMemoryDatabase("TurmaAtualizadaTest"))
            .AddMassTransitTestHarness(cfg => cfg.AddConsumer<TurmaAtualizadaConsumer>())
            .BuildServiceProvider(true);

        var db = provider.GetRequiredService<GradeDbContext>();
        db.TurmasProjecao.Add(new TurmaProjecao { Id = id, Nome = "Antiga", Ano = 1, Turno = "Manhã" });
        await db.SaveChangesAsync();

        var harness = provider.GetRequiredService<ITestHarness>();
        await harness.Start();

        try
        {
            await harness.Bus.Publish(new TurmaAtualizadaEvent(id, "Atualizada", 2, "Noite"));

            var turma = await db.TurmasProjecao.FindAsync(id);
            turma!.Nome.Should().Be("Atualizada");
            turma.Turno.Should().Be("Noite");
        }
        finally
        {
            await harness.Stop();
        }
    }
}
