using Usuario.Domain.Abstractions;

namespace Usuario.Domain.Usuarios;

public record NombreUsuario
{
    public string Value { get; init; }

    private NombreUsuario(string value)
    {
        Value = value;
    }

    public static Result<NombreUsuario> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
           return Result.Failure<NombreUsuario>(UsuarioErrores.NombreUsuarioVacio);
        }
        return new NombreUsuario(value);
    }
}