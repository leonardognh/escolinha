using FluentAssertions;
using Moq;
using Grade.Application.Commands;
using Grade.Application.Handlers;
using Grade.Domain.Entities;
using Grade.Domain.Enums;
using Grade.Domain.Interfaces;

namespace Grade.Tests.Handlers;

public class CreateGradeHorariosHandlerTests
{
    [Fact]
    public async Task Handle_DeveCriarGradeHorariosERetornarId()
    {
        var repo = new Mock<IGradeHorariosRepository>();
        var handler = new CreateGradeHorariosHandler(repo.Object);

        var command = new CreateGradeHorariosCommand
        {
            TurmaId = Guid.NewGuid(),
            Bimestre = 1,
            DiaSemana = DiaSemana.Quarta,
            HorarioInicio = new TimeSpan(8, 0, 0),
            HorarioFim = new TimeSpan(9, 0, 0),
            MateriaId = Guid.NewGuid(),
            ProfessorId = Guid.NewGuid()
        };

        var result = await handler.Handle(command, CancellationToken.None);

        result.Should().NotBeEmpty();
        repo.Verify(r => r.AddAsync(It.IsAny<GradeHorarios>()), Times.Once);
    }
}
