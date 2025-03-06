namespace Usuario.Domain.Roles;

public interface IRolRepository
{
    Task<Rol?> GetByNameAsync(string nombreRol, CancellationToken cancellationToken);
}