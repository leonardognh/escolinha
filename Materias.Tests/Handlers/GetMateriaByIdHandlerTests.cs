using FluentAssertions;
using Materias.Application.Handlers;
using Materias.Application.Queries;
using Materias.Domain.Entities;
using Materias.Domain.Interfaces;
using Moq;

public class GetMateriaByIdHandlerTests
{
    [Fact]
    public async Task Deve_Retornar_MateriaDto()
    {
        var materia = new Materia
        {
            Id = Guid.NewGuid(),
            Nome = "Física",
            Descricao = "Avançada",
            CargaHoraria = 120
        };

        var repo = new Mock<IMateriaRepository>();
        repo.Setup(r => r.GetByIdAsync(materia.Id)).ReturnsAsync(materia);

        var handler = new GetMateriaByIdHandler(repo.Object);
        var result = await handler.Handle(new GetMateriaByIdQuery(materia.Id), default);

        result.Should().NotBeNull();
        result!.Nome.Should().Be("Física");
    }
}
