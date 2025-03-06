using MediatR;

namespace Cursos.Application.ExteralEvents.Matriculas.MatriculaCreada;

public sealed record MatriculaCreatedIntegrationEvent
(
    Guid MatriculaId,
    Guid CursoId
) : INotification;