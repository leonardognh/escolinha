using FluentAssertions;
using Moq;
using Grade.Application.Handlers;
using Grade.Application.Queries;
using Grade.Domain.Entities;
using Grade.Domain.Enums;
using Grade.Domain.Interfaces;

namespace Grade.Tests.Handlers;

public class GetGradeHorarioByIdHandlerTests
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
            HorarioInicio = TimeSpan.FromHours(8),
            HorarioFim = TimeSpan.FromHours(9),
            MateriaId = Guid.NewGuid(),
            ProfessorId = Guid.NewGuid()
        };

        var repo = new Mock<IGradeHorarioRepository>();
        repo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(grade);

        var handler = new GetGradeHorarioByIdHandler(repo.Object);

        var result = await handler.Handle(new GetGradeHorarioByIdQuery(id), CancellationToken.None);

        result.Should().NotBeNull();
        result!.DiaSemana.Should().Be(DiaSemana.Quarta);
    }

    [Fact]
    public async Task Handle_DeveRetornarNull_SeNaoEncontrado()
    {
        var repo = new Mock<IGradeHorarioRepository>();
        var handler = new GetGradeHorarioByIdHandler(repo.Object);

        var result = await handler.Handle(new GetGradeHorarioByIdQuery(Guid.NewGuid()), CancellationToken.None);

        result.Should().BeNull();
    }
}
