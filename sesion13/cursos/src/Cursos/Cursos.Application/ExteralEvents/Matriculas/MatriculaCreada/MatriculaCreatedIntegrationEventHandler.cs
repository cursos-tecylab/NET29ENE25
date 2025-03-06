using Cursos.Application.Cursos.Events;
using Cursos.Application.Service;
using Cursos.Domain.Cursos;
using MediatR;

namespace Cursos.Application.ExteralEvents.Matriculas.MatriculaCreada;

internal sealed class MatriculaCreatedIntegrationEventHandler
    : INotificationHandler<MatriculaCreatedIntegrationEvent>
{

    private readonly ICursoRepository _cursoRepository;
    private readonly IEventBus _eventBus;

    public MatriculaCreatedIntegrationEventHandler(ICursoRepository cursoRepository, IEventBus eventBus)
    {
        _cursoRepository = cursoRepository;
        _eventBus = eventBus;
    }

    public async Task Handle(MatriculaCreatedIntegrationEvent notification, CancellationToken cancellationToken)
    {
       var curso = await _cursoRepository.GetByIdAsync(notification.CursoId, cancellationToken);

       if (curso is null)
       {
          return;
       }

       if (!curso.TieneCupoDisponible())
       {
          _eventBus.Publish(new CursoSinCupoDisponibleIntegrationEvent(notification.MatriculaId));
          return;
       }

       curso.RestarCupo();
       await _cursoRepository.UpdateAsync(notification.CursoId, curso, cancellationToken);
       _eventBus.Publish(new CursoConCupoDisponibleIntegrationEvent(notification.MatriculaId));

    }
}