using Moq;
using Grade.Application.Commands;
using Grade.Application.Handlers;
using Grade.Domain.Interfaces;

namespace Grade.Tests.Handlers;

public class DeleteGradeHorarioHandlerTests
{
    [Fact]
    public async Task Handle_DeveChamarDelete()
    {
        var repo = new Mock<IGradeHorarioRepository>();
        var handler = new DeleteGradeHorarioHandler(repo.Object);

        var id = Guid.NewGuid();
        var command = new DeleteGradeHorarioCommand(id);

        await handler.Handle(command, CancellationToken.None);

        repo.Verify(r => r.DeleteAsync(id), Times.Once);
    }
}
