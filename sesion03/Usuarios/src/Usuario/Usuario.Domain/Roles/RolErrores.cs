using Usuario.Domain.Abstractions;

namespace Usuario.Domain.Roles;

public static class RolErrores
{
    public static Error NoEncontrado = new 
    (
        "RolErrores.NoEncontrado",
        "El rol no fue encontrado"
    );
}