using FluentAssertions;
using Moq;
using Professores.Application.Commands;
using Professores.Application.Handlers;
using Professores.Domain.Interfaces;

namespace Professores.Tests.Handlers;

public class DeleteProfessorHandlerTests
{
    [Fact]
    public async Task Handle_DeveChamarDeleteAsync()
    {
        var repositoryMock = new Mock<IProfessorRepository>();
        var handler = new DeleteProfessorHandler(repositoryMock.Object);

        var id = Guid.NewGuid();
        var command = new DeleteProfessorCommand(id);

        await handler.Handle(command, CancellationToken.None);

        repositoryMock.Verify(r => r.DeleteAsync(id), Times.Once);
    }
}
