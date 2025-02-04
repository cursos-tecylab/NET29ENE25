using Usuario.Domain.Abstractions;

namespace Usuario.Domain.Roles;

public class Rol : Entity
{
    public string? NombreRol { get; private set; }
    public string Descripcion { get; private set; }

    private Rol(
        Guid id, 
        string nombreRol, 
        string descripcion) : base(id)
    {
        NombreRol = nombreRol;
        Descripcion = descripcion;
    }

    public static Result<Rol> Create(
        string nombreRol,
        string descripcion)
    {
        return Result.Success(new Rol(
            Guid.NewGuid(),
            nombreRol,
            descripcion
        ));
    }
}