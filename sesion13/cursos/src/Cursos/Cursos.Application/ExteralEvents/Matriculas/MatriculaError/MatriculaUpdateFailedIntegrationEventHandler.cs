using Cursos.Domain.Cursos;
using MediatR;

namespace Cursos.Application.ExteralEvents.Matriculas.MatriculaError;

internal sealed class MatriculaUpdateFailedIntegrationEventHandler : INotificationHandler<MatriculaUpdateFailedIntegrationEvent>
{
    private readonly ICursoRepository _cursoRepository;

    public MatriculaUpdateFailedIntegrationEventHandler(ICursoRepository cursoRepository)
    {
        _cursoRepository = cursoRepository;
    }

    public async Task Handle(MatriculaUpdateFailedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        var curso = await _cursoRepository.GetByIdAsync(notification.CursoId, cancellationToken);

       if (curso is null)
       {
          return;
       }

        curso.SumarCupo();
       await _cursoRepository.UpdateAsync(notification.CursoId,curso, cancellationToken);


    }
}