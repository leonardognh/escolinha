using FluentAssertions;
using Materias.Application.Handlers;
using Materias.Application.Queries;
using Materias.Domain.Entities;
using Materias.Domain.Interfaces;
using Moq;

public class GetAllMateriasHandlerTests
{
    [Fact]
    public async Task Deve_Retornar_Materias_Paginadas()
    {
        var materias = new List<Materia>
        {
            new() { Id = Guid.NewGuid(), Nome = "História" }
        };

        var repo = new Mock<IMateriaRepository>();
        repo.Setup(r => r.GetPagedAsync(1, 10)).ReturnsAsync((materias, 1));

        var handler = new GetAllMateriasHandler(repo.Object);
        var result = await handler.Handle(new GetAllMateriasQuery(1, 10), default);

        result.Items.Should().HaveCount(1);
        result.TotalItems.Should().Be(1);
        result.TotalPages.Should().Be(1);
    }
}
