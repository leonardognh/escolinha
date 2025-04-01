using FluentAssertions;
using MassTransit;
using Moq;
using Turmas.Application.Commands;
using Turmas.Application.Handlers;
using Turmas.Domain.Entities;
using Turmas.Domain.Interfaces;

public class UpdateTurmaHandlerTests
{
    [Fact]
    public async Task Deve_Atualizar_Turma()
    {
        var turma = new Turma { Id = Guid.NewGuid(), Nome = "Antiga", Ano = 1 };
        var repo = new Mock<ITurmaRepository>();
        var publish = new Mock<IPublishEndpoint>();

        repo.Setup(r => r.GetByIdAsync(turma.Id)).ReturnsAsync(turma);

        var handler = new UpdateTurmaHandler(repo.Object, publish.Object);

        var command = new UpdateTurmaCommand
        {
            Id = turma.Id,
            Nome = "Nova",
            Ano = 2,
        };

        await handler.Handle(command, CancellationToken.None);

        turma.Nome.Should().Be("Nova");
        turma.Ano.Should().Be(2);
        repo.Verify(r => r.UpdateAsync(turma), Times.Once);
    }
}
