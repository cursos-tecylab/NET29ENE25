using MediatR;

namespace Estudiantes.Application.ExternalEvents.Cursos.CursosSinCupo;
public sealed record CursoSinCupoDisponibleIntegrationEvent (Guid MatriculaId): INotification;