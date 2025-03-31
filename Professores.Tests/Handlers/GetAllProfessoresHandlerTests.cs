using FluentAssertions;
using Moq;
using Professores.Application.DTOs;
using Professores.Application.Handlers;
using Professores.Application.Queries;
using Professores.Domain.Entities;
using Professores.Domain.Interfaces;

namespace Professores.Tests.Handlers;

public class GetAllProfessoresHandlerTests
{
    [Fact]
    public async Task Handle_DeveRetornarProfessoresPaginados()
    {
        var repositoryMock = new Mock<IProfessorRepository>();
        repositoryMock.Setup(r => r.GetPagedAsync(1, 10))
            .ReturnsAsync((new List<Professor>
            {
                new() { Id = Guid.NewGuid(), Nome = "Professor", Email = "professor@email.com" }
            }, 1));

        var handler = new GetAllProfessoresHandler(repositoryMock.Object);

        var result = await handler.Handle(new GetAllProfessoresQuery(1, 10), CancellationToken.None);

        result.Items.Should().HaveCount(1);
        result.TotalItems.Should().Be(1);
        result.TotalPages.Should().Be(1);
    }
}
