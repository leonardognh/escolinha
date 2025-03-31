using Alunos.Application.Handlers;
using Alunos.Application.Queries;
using Alunos.Domain.Entities;
using Alunos.Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace Alunos.Tests.Handlers;

public class GetAlunoByIdHandlerTests
{
    [Fact]
    public async Task Handle_DeveRetornarAluno()
    {
        var alunoId = Guid.NewGuid();
        var aluno = new Aluno { Id = alunoId, Nome = "Aluno", Email = "aluno@email.com" };

        var repo = new Mock<IAlunoRepository>();
        repo.Setup(r => r.GetByIdAsync(alunoId)).ReturnsAsync(aluno);

        var handler = new GetAlunoByIdHandler(repo.Object);

        var result = await handler.Handle(new GetAlunoByIdQuery(alunoId), CancellationToken.None);

        result.Should().NotBeNull();
        result!.Nome.Should().Be("Aluno");
    }

    [Fact]
    public async Task Handle_DeveRetornarNull_SeNaoEncontrado()
    {
        var repo = new Mock<IAlunoRepository>();
        var handler = new GetAlunoByIdHandler(repo.Object);

        var result = await handler.Handle(new GetAlunoByIdQuery(Guid.NewGuid()), CancellationToken.None);

        result.Should().BeNull();
    }
}
