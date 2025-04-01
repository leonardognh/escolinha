using Alunos.Application.Commands;
using Alunos.Domain.Entities;
using Alunos.Domain.Interfaces;
using FluentAssertions;
using MassTransit;
using Moq;

namespace Alunos.Tests.Handlers;

public class CreateAlunoHandlerTests
{
    [Fact]
    public async Task Handle_DeveCriarAlunoERetornarId()
    {
        var repositoryMock = new Mock<IAlunoRepository>();
        var publish = new Mock<IPublishEndpoint>();

        var handler = new CreateAlunoHandler(repositoryMock.Object, publish.Object);

        var command = new CreateAlunoCommand
        {
            Nome = "Aluno",
            TurmaId = Guid.NewGuid()
        };

        var result = await handler.Handle(command, CancellationToken.None);

        result.Should().NotBeEmpty();
        repositoryMock.Verify(r => r.AddAsync(It.IsAny<Aluno>()), Times.Once);
    }
}
