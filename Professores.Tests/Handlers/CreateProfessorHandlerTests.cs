using FluentAssertions;
using MassTransit;
using Moq;
using Professores.Application.Commands;
using Professores.Application.Handlers;
using Professores.Domain.Entities;
using Professores.Domain.Interfaces;

namespace Professores.Tests.Handlers;

public class CreateProfessorHandlerTests
{
    [Fact]
    public async Task Handle_DeveCriarProfessorERetornarId()
    {
        var repositoryMock = new Mock<IProfessorRepository>();
        var publish = new Mock<IPublishEndpoint>();

        var handler = new CreateProfessorHandler(repositoryMock.Object, publish.Object);

        var command = new CreateProfessorCommand
        {
            Nome = "Fulano",
        };

        var result = await handler.Handle(command, CancellationToken.None);

        result.Should().NotBeEmpty();
        repositoryMock.Verify(r => r.AddAsync(It.IsAny<Professor>()), Times.Once);
    }
}
