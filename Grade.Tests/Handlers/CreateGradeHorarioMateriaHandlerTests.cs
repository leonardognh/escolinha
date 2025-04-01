using FluentAssertions;
using Moq;
using Grade.Application.Commands;
using Grade.Domain.Entities;
using Grade.Domain.Interfaces;

namespace Grade.Tests.Handlers;

public class CreateGradeHorarioMateriaHandlerTests
{
    [Fact]
    public async Task Handle_DeveCriarGradeHorariosERetornarId()
    {
        var repo = new Mock<IGradeHorarioMateriaRepository>();
        var handler = new CreateGradeHorarioMateriaHandler(repo.Object);

        var command = new CreateGradeHorarioMateriaCommand
        {
            MateriaId = Guid.NewGuid(),
            ProfessorId = Guid.NewGuid(),
        };

        var result = await handler.Handle(command, CancellationToken.None);

        result.Should().NotBeEmpty();
        repo.Verify(r => r.AddAsync(It.IsAny<GradeHorarioMateria>()), Times.Once);
    }
}
