using FluentAssertions;
using Moq;
using Materias.Application.Commands;
using Materias.Application.Handlers;
using Materias.Domain.Entities;
using Materias.Domain.Interfaces;
using MassTransit;

namespace Materias.Tests.Handlers;

public class CreateMateriaHandlerTests
{
    [Fact]
    public async Task Deve_Criar_Materia_E_Retornar_Id()
    {
        var repo = new Mock<IMateriaRepository>();
        var publish = new Mock<IPublishEndpoint>();

        var handler = new CreateMateriaHandler(repo.Object, publish.Object);

        var command = new CreateMateriaCommand
        {
            Nome = "Matemática",
        };

        var result = await handler.Handle(command, default);

        result.Should().NotBeEmpty();
        repo.Verify(x => x.AddAsync(It.Is<Materia>(m => m.Nome == "Matemática")), Times.Once);
    }
}
