using FluentAssertions;
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
        var handler = new CreateProfessorHandler(repositoryMock.Object);

        var command = new CreateProfessorCommand
        {
            Nome = "Fulano",
            Email = "fulano@email.com",
            Telefone = "11999999999"
        };

        var result = await handler.Handle(command, CancellationToken.None);

        result.Should().NotBeEmpty();
        repositoryMock.Verify(r => r.AddAsync(It.IsAny<Professor>()), Times.Once);
    }
}
