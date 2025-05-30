﻿using MediatR;
using Materias.Application.Commands;
using Materias.Domain.Entities;
using Materias.Domain.Interfaces;
using MassTransit;
using Contracts.Events.Materias;

namespace Materias.Application.Handlers;

public class CreateMateriaHandler : IRequestHandler<CreateMateriaCommand, Guid>
{
    private readonly IMateriaRepository _repository;
    private readonly IPublishEndpoint _publish;

    public CreateMateriaHandler(IMateriaRepository repository, IPublishEndpoint publish)
    {
        _repository = repository;
        _publish = publish;
    }

    public async Task<Guid> Handle(CreateMateriaCommand request, CancellationToken cancellationToken)
    {
        var materia = new Materia
        {
            Id = Guid.NewGuid(),
            Nome = request.Nome,
        };

        await _repository.AddAsync(materia);

        await _publish.Publish(new MateriaCriadaEvent(
            materia.Id, materia.Nome
        ));

        return materia.Id;
    }
}
