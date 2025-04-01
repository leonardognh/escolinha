using FluentAssertions;
using Moq;
using Grade.Application.Handlers;
using Grade.Application.Queries;
using Grade.Domain.Entities;
using Grade.Domain.Enums;
using Grade.Domain.Interfaces;

namespace Grade.Tests.Handlers;

public class GetGradeHorariosByIdHandlerTests
{
    [Fact]
    public async Task Handle_DeveRetornarGrade()
    {
        var id = Guid.NewGuid();
        var grade = new GradeHorario
        {
            Id = id,
            TurmaId = Guid.NewGuid(),
            Bimestre = 1,
            DiaSemana = DiaSemana.Quarta,
        };

        var repo = new Mock<IGradeHorariosRepository>();
        repo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(grade);

        var handler = new GetGradeHorariosByIdHandler(repo.Object);

        var result = await handler.Handle(new GetGradeHorariosByIdQuery(id), CancellationToken.None);

        result.Should().NotBeNull();
        result!.DiaSemana.Should().Be(DiaSemana.Quarta);
    }

    [Fact]
    public async Task Handle_DeveRetornarNull_SeNaoEncontrado()
    {
        var repo = new Mock<IGradeHorariosRepository>();
        var handler = new GetGradeHorariosByIdHandler(repo.Object);

        var result = await handler.Handle(new GetGradeHorariosByIdQuery(Guid.NewGuid()), CancellationToken.None);

        result.Should().BeNull();
    }
}
