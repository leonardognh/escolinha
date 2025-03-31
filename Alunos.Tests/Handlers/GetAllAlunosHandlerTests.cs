using Alunos.Application.Handlers;
using Alunos.Application.Queries;
using Alunos.Domain.Entities;
using Alunos.Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace Alunos.Tests.Handlers;

public class GetAllAlunosHandlerTests
{
    [Fact]
    public async Task Handle_DeveRetornarListaComPaginacao()
    {
        var repo = new Mock<IAlunoRepository>();
        repo.Setup(r => r.GetPagedAsync(1, 10)).ReturnsAsync((
            new List<Aluno>
            {
                new Aluno { Id = Guid.NewGuid(), Nome = "Aluno", Email = "aluno@email.com" }
            }, 1));

        var handler = new GetAllAlunosHandler(repo.Object);

        var result = await handler.Handle(new GetAllAlunosQuery(1, 10), CancellationToken.None);

        result.Items.Should().HaveCount(1);
        result.TotalItems.Should().Be(1);
        result.TotalPages.Should().Be(1);
    }
}
