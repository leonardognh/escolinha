using Moq;
using Turmas.Application.Commands;
using Turmas.Application.Handlers;
using Turmas.Domain.Interfaces;

public class DeleteTurmaHandlerTests
{
    [Fact]
    public async Task Deve_Chamar_DeleteAsync()
    {
        var repo = new Mock<ITurmaRepository>();
        var handler = new DeleteTurmaHandler(repo.Object);

        var id = Guid.NewGuid();

        await handler.Handle(new DeleteTurmaCommand(id), CancellationToken.None);

        repo.Verify(r => r.DeleteAsync(id), Times.Once);
    }
}
