using Alunos.Application.Commands;
using Alunos.Application.Handlers;
using Alunos.Domain.Entities;
using Alunos.Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace Alunos.Tests.Handlers;

public class CreateAlunoHandlerTests
{
    [Fact]
    public async Task Handle_DeveCriarAlunoERetornarId()
    {
        var repositoryMock = new Mock<IAlunoRepository>();
        var handler = new CreateAlunoHandler(repositoryMock.Object);

        var command = new CreateAlunoCommand
        {
            Nome = "Aluno",
            Email = "aluno@email.com",
            DataNascimento = new DateTime(2005, 5, 1),
            TurmaId = Guid.NewGuid()
        };

        var result = await handler.Handle(command, CancellationToken.None);

        result.Should().NotBeEmpty();
        repositoryMock.Verify(r => r.AddAsync(It.IsAny<Aluno>()), Times.Once);
    }
}
