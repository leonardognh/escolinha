using FluentAssertions;
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
        var professor = new Professor { Id = Guid.NewGuid(), Nome = "Old", Email = "old@email.com" };
        var repositoryMock = new Mock<IProfessorRepository>();
        repositoryMock.Setup(r => r.GetByIdAsync(professor.Id)).ReturnsAsync(professor);

        var handler = new UpdateProfessorHandler(repositoryMock.Object);

        var command = new UpdateProfessorCommand
        {
            Id = professor.Id,
            Nome = "Novo",
            Email = "novo@email.com",
            Telefone = "11999999999"
        };

        await handler.Handle(command, CancellationToken.None);

        professor.Nome.Should().Be("Novo");
        repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Professor>()), Times.Once);
    }

    [Fact]
    public async Task Handle_DeveLancarExcecaoQuandoProfessorNaoExiste()
    {
        var repositoryMock = new Mock<IProfessorRepository>();
        repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Professor?)null);

        var handler = new UpdateProfessorHandler(repositoryMock.Object);

        var command = new UpdateProfessorCommand
        {
            Id = Guid.NewGuid(),
            Nome = "Teste",
            Email = "teste@email.com"
        };

        var act = async () => await handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<Exception>().WithMessage("Professor não encontrado.");
    }
}
