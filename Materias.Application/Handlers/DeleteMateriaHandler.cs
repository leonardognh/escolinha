using MediatR;
using Materias.Application.Commands;
using Materias.Domain.Interfaces;
using MassTransit;
using IntegracaoMicroservicos.Contracts.Events.Materias;

namespace Materias.Application.Handlers;

public class DeleteMateriaHandler : IRequestHandler<DeleteMateriaCommand>
{
    private readonly IMateriaRepository _repository;
    private readonly IPublishEndpoint _publish;

    public DeleteMateriaHandler(IMateriaRepository repository, IPublishEndpoint publish)
    {
        _repository = repository;
        _publish = publish;
    }

    public async Task<Unit> Handle(DeleteMateriaCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(request.Id);

        await _publish.Publish(new MateriaRemovidaEvent(request.Id));

        return Unit.Value;
    }
}
