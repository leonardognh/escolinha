using FluentAssertions;
using MassTransit;
using Moq;
using Professores.Application.Commands;
using Professores.Application.Handlers;
using Professores.Domain.Entities;
using Professores.Domain.Interfaces;

namespace Professores.Tests.Handlers;

public class UpdateProfessorHandlerTests
{
    [Fact]
    public async Task Handle_DeveAtualizarProfessorQuandoEncontrado()
    {
        var professor = new Professor { Id = Guid.NewGuid(), Nome = "Old" };
        var repositoryMock = new Mock<IProfessorRepository>();
        var publish = new Mock<IPublishEndpoint>();

        repositoryMock.Setup(r => r.GetByIdAsync(professor.Id)).ReturnsAsync(professor);

        var handler = new UpdateProfessorHandler(repositoryMock.Object, publish.Object);

        var command = new UpdateProfessorCommand
        {
            Id = professor.Id,
            Nome = "Novo",
        };

        await handler.Handle(command, CancellationToken.None);

        professor.Nome.Should().Be("Novo");
        repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Professor>()), Times.Once);
    }

    [Fact]
    public async Task Handle_DeveLancarExcecaoQuandoProfessorNaoExiste()
    {
        var repositoryMock = new Mock<IProfessorRepository>();
        var publish = new Mock<IPublishEndpoint>();

        repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Professor?)null);

        var handler = new UpdateProfessorHandler(repositoryMock.Object, publish.Object);

        var command = new UpdateProfessorCommand
        {
            Id = Guid.NewGuid(),
            Nome = "Teste",
        };

        var act = async () => await handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<Exception>().WithMessage("Professor não encontrado.");
    }
}
