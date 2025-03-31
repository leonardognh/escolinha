using FluentAssertions;
using Grade.API.Consumers.Alunos;
using Grade.Infrastructure.Persistence;
using IntegracaoMicroservicos.Contracts.Events.Alunos;
using MassTransit;
using MassTransit.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Grade.Tests.Consumers.Alunos;
public class AlunoCriadoConsumerTests
{
    [Fact]
    public async Task Deve_Salvar_Aluno_Projecao()
    {
        var provider = new ServiceCollection()
            .AddDbContext<GradeDbContext>(opt => opt.UseInMemoryDatabase("AlunoCriadoTest"))
            .AddMassTransitTestHarness(cfg => cfg.AddConsumer<AlunoCriadoConsumer>())
            .BuildServiceProvider(true);

        var harness = provider.GetRequiredService<ITestHarness>();
        await harness.Start();

        try
        {
            var turmaId = Guid.NewGuid();
            var aluno = new AlunoCriadoEvent(Guid.NewGuid(), "João", turmaId);

            await harness.Bus.Publish(aluno);

            var db = provider.GetRequiredService<GradeDbContext>();
            var salvo = await db.AlunosProjecao.FindAsync(aluno.Id);

            salvo.Should().NotBeNull();
            salvo!.TurmaId.Should().Be(turmaId);
        }
        finally
        {
            await harness.Stop();
        }
    }
}
