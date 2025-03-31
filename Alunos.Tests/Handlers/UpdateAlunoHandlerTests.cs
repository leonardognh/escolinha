using Alunos.Application.Commands;
using Alunos.Application.Handlers;
using Alunos.Domain.Entities;
using Alunos.Domain.Interfaces;
using FluentAssertions;
using MassTransit;
using Moq;

namespace Alunos.Tests.Handlers;

public class UpdateAlunoHandlerTests
{
    [Fact]
    public async Task Handle_DeveAtualizarAluno()
    {
        var aluno = new Aluno { Id = Guid.NewGuid(), Nome = "Antigo", Email = "antigo@email.com" };
        var repositoryMock = new Mock<IAlunoRepository>();
        var publish = new Mock<IPublishEndpoint>();

        repositoryMock.Setup(r => r.GetByIdAsync(aluno.Id)).ReturnsAsync(aluno);

        var handler = new UpdateAlunoHandler(repositoryMock.Object, publish.Object);

        var command = new UpdateAlunoCommand
        {
            Id = aluno.Id,
            Nome = "Novo",
            Email = "novo@email.com",
            DataNascimento = DateTime.Today,
            TurmaId = Guid.NewGuid()
        };

        await handler.Handle(command, CancellationToken.None);

        aluno.Nome.Should().Be("Novo");
        repositoryMock.Verify(r => r.UpdateAsync(aluno), Times.Once);
    }

    [Fact]
    public async Task Handle_DeveLancarExcecao_SeAlunoNaoExiste()
    {
        var repositoryMock = new Mock<IAlunoRepository>();
        var publish = new Mock<IPublishEndpoint>();

        repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Aluno?)null);

        var handler = new UpdateAlunoHandler(repositoryMock.Object, publish.Object);

        var command = new UpdateAlunoCommand
        {
            Id = Guid.NewGuid(),
            Nome = "Teste",
            Email = "teste@email.com",
            DataNascimento = DateTime.Today,
            TurmaId = Guid.NewGuid()
        };

        var act = async () => await handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<Exception>().WithMessage("Aluno não encontrado.");
    }
}
