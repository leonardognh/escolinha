using FluentAssertions;
using MassTransit;
using Moq;
using Turmas.Application.Commands;
using Turmas.Application.Handlers;
using Turmas.Domain.Entities;
using Turmas.Domain.Interfaces;

namespace Turmas.Tests.Handlers;

public class CreateTurmaHandlerTests
{
    [Fact]
    public async Task Deve_Criar_Turma_E_Retornar_Id()
    {
        // Arrange
        var repo = new Mock<ITurmaRepository>();
        var publish = new Mock<IPublishEndpoint>();

        var handler = new CreateTurmaHandler(repo.Object, publish.Object);

        var command = new CreateTurmaCommand
        {
            Nome = "1ºA",
            Ano = 1,
            Turno = "Manhã"
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeEmpty();
        repo.Verify(x => x.AddAsync(It.Is<Turma>(t => t.Nome == "1ºA")), Times.Once);
    }
}
