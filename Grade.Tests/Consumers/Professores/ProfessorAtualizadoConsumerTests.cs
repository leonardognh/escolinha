using FluentAssertions;
using Grade.API.Consumers.Professores;
using Grade.Infrastructure.Persistence;
using Contracts.Events.Professores;
using MassTransit;
using MassTransit.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Grade.Domain.Entities.Projecao;

namespace Grade.Tests.Consumers.Professores;

public class ProfessorAtualizadoConsumerTests
{
    [Fact]
    public async Task Deve_Atualizar_Professor_Projecao()
    {
        var professorId = Guid.NewGuid();

        var provider = new ServiceCollection()
            .AddDbContext<GradeDbContext>(opt =>
                opt.UseInMemoryDatabase("ProfessorAtualizadoTest"))
            .AddMassTransitTestHarness(cfg =>
            {
                cfg.AddConsumer<ProfessorAtualizadoConsumer>();
            })
            .BuildServiceProvider(true);

        var db = provider.GetRequiredService<GradeDbContext>();
        db.ProfessoresProjecao.Add(new ProfessorProjecao
        {
            Id = professorId,
            Nome = "Antigo Nome",
        });
        await db.SaveChangesAsync();

        var harness = provider.GetRequiredService<ITestHarness>();
        await harness.Start();

        try
        {
            var evento = new ProfessorAtualizadoEvent(professorId, "Novo Nome");

            await harness.Bus.Publish(evento);

            var professor = await db.ProfessoresProjecao.FindAsync(professorId);

            professor.Should().NotBeNull();
            professor!.Nome.Should().Be("Novo Nome");
        }
        finally
        {
            await harness.Stop();
        }
    }
}
