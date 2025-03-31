using MassTransit;
using Materias.Application.Commands;
using Materias.Application.Handlers;
using Materias.Domain.Interfaces;
using Moq;

public class DeleteMateriaHandlerTests
{
    [Fact]
    public async Task Deve_Chamar_DeleteAsync()
    {
        var repo = new Mock<IMateriaRepository>();
        var publish = new Mock<IPublishEndpoint>();

        var handler = new DeleteMateriaHandler(repo.Object, publish.Object);

        var id = Guid.NewGuid();

        await handler.Handle(new DeleteMateriaCommand(id), default);

        repo.Verify(r => r.DeleteAsync(id), Times.Once);
    }
}
