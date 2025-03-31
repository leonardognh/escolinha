using FluentAssertions;
using Moq;
using Turmas.Application.Handlers;
using Turmas.Application.Queries;
using Turmas.Domain.Entities;
using Turmas.Domain.Interfaces;

public class GetAllTurmasHandlerTests
{
    [Fact]
    public async Task Deve_Retornar_Turmas_Paginadas()
    {
        var turmas = new List<Turma> { new() { Id = Guid.NewGuid(), Nome = "3ºC", Ano = 3 } };
        var repo = new Mock<ITurmaRepository>();
        repo.Setup(r => r.GetPagedAsync(1, 10)).ReturnsAsync((turmas, 1));

        var handler = new GetAllTurmasHandler(repo.Object);

        var result = await handler.Handle(new GetAllTurmasQuery(1, 10), CancellationToken.None);

        result.Items.Should().HaveCount(1);
        result.TotalItems.Should().Be(1);
        result.TotalPages.Should().Be(1);
    }
}
