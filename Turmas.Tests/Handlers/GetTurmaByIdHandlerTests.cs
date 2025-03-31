using FluentAssertions;
using Moq;
using Turmas.Application.Handlers;
using Turmas.Application.Queries;
using Turmas.Domain.Entities;
using Turmas.Domain.Interfaces;

public class GetTurmaByIdHandlerTests
{
    [Fact]
    public async Task Deve_Retornar_TurmaDto()
    {
        var turma = new Turma { Id = Guid.NewGuid(), Nome = "2ºB", Ano = 2, Turno = "Tarde" };
        var repo = new Mock<ITurmaRepository>();
        repo.Setup(r => r.GetByIdAsync(turma.Id)).ReturnsAsync(turma);

        var handler = new GetTurmaByIdHandler(repo.Object);

        var result = await handler.Handle(new GetTurmaByIdQuery(turma.Id), CancellationToken.None);

        result.Should().NotBeNull();
        result!.Nome.Should().Be("2ºB");
    }
}
