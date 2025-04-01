using FluentAssertions;
using Moq;
using Professores.Application.DTOs;
using Professores.Application.Handlers;
using Professores.Application.Queries;
using Professores.Domain.Entities;
using Professores.Domain.Interfaces;

namespace Professores.Tests.Handlers;

public class GetProfessorByIdHandlerTests
{
    [Fact]
    public async Task Handle_DeveRetornarProfessorQuandoEncontrado()
    {
        var professorId = Guid.NewGuid();
        var repositoryMock = new Mock<IProfessorRepository>();
        repositoryMock.Setup(r => r.GetByIdAsync(professorId))
            .ReturnsAsync(new Professor { Id = professorId, Nome = "Ana" });

        var handler = new GetProfessorByIdHandler(repositoryMock.Object);

        var result = await handler.Handle(new GetProfessorByIdQuery(professorId), CancellationToken.None);

        result.Should().NotBeNull();
        result!.Nome.Should().Be("Ana");
    }

    [Fact]
    public async Task Handle_DeveRetornarNullQuandoNaoEncontrado()
    {
        var repositoryMock = new Mock<IProfessorRepository>();
        var handler = new GetProfessorByIdHandler(repositoryMock.Object);

        var result = await handler.Handle(new GetProfessorByIdQuery(Guid.NewGuid()), CancellationToken.None);

        result.Should().BeNull();
    }
}
