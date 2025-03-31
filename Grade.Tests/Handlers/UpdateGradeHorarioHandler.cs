using FluentAssertions;
using Moq;
using Grade.Application.Commands;
using Grade.Application.Handlers;
using Grade.Domain.Entities;
using Grade.Domain.Enums;
using Grade.Domain.Interfaces;

namespace Grade.Tests.Handlers;

public class UpdateGradeHorarioHandlerTests
{
    [Fact]
    public async Task Handle_DeveAtualizarGradeHorario()
    {
        var grade = new GradeHorario
        {
            Id = Guid.NewGuid(),
            TurmaId = Guid.NewGuid(),
            Bimestre = 1,
            DiaSemana = DiaSemana.Segunda,
            HorarioInicio = TimeSpan.FromHours(8),
            HorarioFim = TimeSpan.FromHours(9),
            MateriaId = Guid.NewGuid(),
            ProfessorId = Guid.NewGuid()
        };

        var repo = new Mock<IGradeHorarioRepository>();
        repo.Setup(r => r.GetByIdAsync(grade.Id)).ReturnsAsync(grade);

        var handler = new UpdateGradeHorarioHandler(repo.Object);

        var command = new UpdateGradeHorarioCommand
        {
            Id = grade.Id,
            TurmaId = grade.TurmaId,
            Bimestre = 2,
            DiaSemana = DiaSemana.Quinta,
            HorarioInicio = TimeSpan.FromHours(10),
            HorarioFim = TimeSpan.FromHours(11),
            MateriaId = Guid.NewGuid(),
            ProfessorId = Guid.NewGuid()
        };

        await handler.Handle(command, CancellationToken.None);

        grade.Bimestre.Should().Be(2);
        grade.DiaSemana.Should().Be(DiaSemana.Quinta);
        repo.Verify(r => r.UpdateAsync(grade), Times.Once);
    }

    [Fact]
    public async Task Handle_DeveLancarExcecao_SeNaoEncontrado()
    {
        var repo = new Mock<IGradeHorarioRepository>();
        repo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((GradeHorario?)null);

        var handler = new UpdateGradeHorarioHandler(repo.Object);

        var command = new UpdateGradeHorarioCommand
        {
            Id = Guid.NewGuid(),
            TurmaId = Guid.NewGuid(),
            Bimestre = 1,
            DiaSemana = DiaSemana.Terca,
            HorarioInicio = TimeSpan.FromHours(8),
            HorarioFim = TimeSpan.FromHours(9),
            MateriaId = Guid.NewGuid(),
            ProfessorId = Guid.NewGuid()
        };

        var act = async () => await handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<Exception>().WithMessage("Grade não encontrada.");
    }
}
