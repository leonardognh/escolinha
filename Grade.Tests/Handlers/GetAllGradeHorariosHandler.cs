﻿using FluentAssertions;
using Moq;
using Grade.Application.Handlers;
using Grade.Application.Queries;
using Grade.Domain.Entities;
using Grade.Domain.Enums;
using Grade.Domain.Interfaces;

namespace Grade.Tests.Handlers;

public class GetAllGradeHorariossHandlerTests
{
    [Fact]
    public async Task Handle_DeveRetornarListaPaginada()
    {
        var repo = new Mock<IGradeHorariosRepository>();
        repo.Setup(r => r.GetPagedAsync(1, 10)).ReturnsAsync((
            new List<GradeHorarios>
            {
                new() { Id = Guid.NewGuid(), DiaSemana = DiaSemana.Segunda }
            }, 1));

        var handler = new GetAllGradeHorariossHandler(repo.Object);

        var result = await handler.Handle(new GetAllGradeHorariossQuery(1, 10), CancellationToken.None);

        result.Items.Should().HaveCount(1);
        result.TotalItems.Should().Be(1);
        result.TotalPages.Should().Be(1);
    }
}
