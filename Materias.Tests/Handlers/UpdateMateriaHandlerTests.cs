using FluentAssertions;
using MassTransit;
using Materias.Application.Commands;
using Materias.Application.Handlers;
using Materias.Domain.Entities;
using Materias.Domain.Interfaces;
using Moq;

public class UpdateMateriaHandlerTests
{
    [Fact]
    public async Task Deve_Atualizar_Materia()
    {
        var materia = new Materia
        {
            Id = Guid.NewGuid(),
            Nome = "Antiga",
        };

        var repo = new Mock<IMateriaRepository>();
        var publish = new Mock<IPublishEndpoint>();

        repo.Setup(r => r.GetByIdAsync(materia.Id)).ReturnsAsync(materia);

        var handler = new UpdateMateriaHandler(repo.Object, publish.Object);

        var command = new UpdateMateriaCommand
        {
            Id = materia.Id,
            Nome = "Nova",
        };

        await handler.Handle(command, default);

        materia.Nome.Should().Be("Nova");
        repo.Verify(r => r.UpdateAsync(materia), Times.Once);
    }
}
