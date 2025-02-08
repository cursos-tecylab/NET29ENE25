namespace Usuario.Domain.Usuarios;

public interface IUsuarioRepository
{
    Task<Usuario?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    void Add(Usuario usuario);

    void Delete(Usuario usuario);   
}