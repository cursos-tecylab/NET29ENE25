using MediatR;

namespace Estudiantes.Application.ExternalEvents.Cursos.CursosConCupo;

public sealed record CursoConCupoDisponibleIntegrationEvent (Guid MatriculaId): INotification;
