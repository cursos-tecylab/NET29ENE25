namespace Docentes.Application.Service;

public interface ICursosService
{
    Task<bool> CursoExisteAsync(Guid cursoId, CancellationToken cancellationToken);
}