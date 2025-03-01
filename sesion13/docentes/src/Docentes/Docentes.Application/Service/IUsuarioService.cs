namespace Docentes.Application.Service;

public interface IUsuarioService
{
    Task<bool> UsuarioExisteAsync(Guid usuarioId, CancellationToken cancellationToken);
}