﻿using MediatR;
using Materias.Application.Commands;
using Materias.Domain.Interfaces;
using MassTransit;
using Contracts.Events.Materias;

namespace Materias.Application.Handlers;

public class UpdateMateriaHandler : IRequestHandler<UpdateMateriaCommand>
{
    private readonly IMateriaRepository _repository;
    private readonly IPublishEndpoint _publish;

    public UpdateMateriaHandler(IMateriaRepository repository, IPublishEndpoint publish)
    {
        _repository = repository;
        _publish = publish;
    }

    public async Task<Unit> Handle(UpdateMateriaCommand request, CancellationToken cancellationToken)
    {
        var materia = await _repository.GetByIdAsync(request.Id)
            ?? throw new Exception("Matéria não encontrada.");

        materia.Nome = request.Nome;

        await _repository.UpdateAsync(materia);

        await _publish.Publish(new MateriaAtualizadaEvent(
            materia.Id, materia.Nome
        ));

        return Unit.Value;
    }
}
