using MediatR;

namespace Cursos.Application.ExteralEvents.Matriculas.MatriculaError;

public sealed record MatriculaUpdateFailedIntegrationEvent
(
    Guid CursoId
) : INotification;