using Estudiantes.Application.Matriculas.Events;
using Estudiantes.Application.Services;
using Estudiantes.Domain.Matriculas;
using Estudiantes.Domain.Matriculas.Events;
using MediatR;

namespace Estudiantes.Application.Matriculas.CrearMatricula;

public class CrearMatriculaDomainEventHandler: INotificationHandler<MatriculaCreateDomainEvent>
{
    private readonly IEventBus _eventBus;
    private readonly IMatriculaRepository _matriculaRepository;

    public CrearMatriculaDomainEventHandler(IEventBus eventBus, IMatriculaRepository matriculaRepository)
    {
        _eventBus = eventBus;
        _matriculaRepository = matriculaRepository;
    }

    public async Task Handle(MatriculaCreateDomainEvent notification, CancellationToken cancellationToken)
    {
        var matricula = await _matriculaRepository.GetByIdAsync(notification.MatriculaId,cancellationToken);

        if(matricula is null){
            return;
        }
        _eventBus.Publish(new MatriculaCreatedIntegrationEvent(notification.MatriculaId,matricula.Programacion!.CursoId)); 
    }
}