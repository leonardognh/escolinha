﻿using FluentAssertions;
using Grade.API.Consumers.Alunos;
using Grade.Domain.Entities;
using Grade.Infrastructure.Persistence;
using IntegracaoMicroservicos.Contracts.Events.Alunos;
using MassTransit;
using MassTransit.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Grade.Tests.Consumers.Alunos;

public class AlunoTransferidoConsumerTests
{
    [Fact]
    public async Task Deve_Atualizar_Turma_Do_Aluno()
    {
        var alunoId = Guid.NewGuid();

        var provider = new ServiceCollection()
            .AddDbContext<GradeDbContext>(opt => opt.UseInMemoryDatabase("AlunoTransferidoTest"))
            .AddMassTransitTestHarness(cfg => cfg.AddConsumer<AlunoTransferidoConsumer>())
            .BuildServiceProvider(true);

        var db = provider.GetRequiredService<GradeDbContext>();
        var turmaId = Guid.NewGuid();
        db.AlunosProjecao.Add(new AlunoProjecao { Id = alunoId, Nome = "João", TurmaId = turmaId });
        await db.SaveChangesAsync();

        var harness = provider.GetRequiredService<ITestHarness>();
        await harness.Start();

        try
        {
            var evento = new AlunoTransferidoEvent(alunoId, Guid.NewGuid(), turmaId);
            await harness.Bus.Publish(evento);

            var aluno = await db.AlunosProjecao.FindAsync(alunoId);
            aluno!.TurmaId.Should().Be(turmaId);
        }
        finally
        {
            await harness.Stop();
        }
    }
}
