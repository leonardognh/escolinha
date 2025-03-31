using Alunos.Application.Commands;
using Alunos.Application.Handlers;
using Alunos.Domain.Interfaces;
using Moq;

namespace Alunos.Tests.Handlers;

public class DeleteAlunoHandlerTests
{
    [Fact]
    public async Task Handle_DeveExcluirAluno()
    {
        var repo = new Mock<IAlunoRepository>();
        var handler = new DeleteAlunoHandler(repo.Object);

        var id = Guid.NewGuid();
        var command = new DeleteAlunoCommand(id);

        await handler.Handle(command, CancellationToken.None);

        repo.Verify(r => r.DeleteAsync(id), Times.Once);
    }
}
