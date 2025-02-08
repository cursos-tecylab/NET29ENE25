using System.Text.RegularExpressions;
using Usuario.Domain.Abstractions;

namespace Usuario.Domain.Usuarios;

public record CorreoElectronico
{
    public string Value { get; init; }

    private CorreoElectronico(string value)
    {
        Value = value;
    }

    public static Result<CorreoElectronico> Create(string value)
    {
        if (EsCorreoValido(value))
        {
            return Result.Failure<CorreoElectronico>(UsuarioErrores.CorreoElectronicoInvalido);
        }
        return new CorreoElectronico(value);
    }

    public static bool EsCorreoValido(string value)
    {
        const string emailRegex = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$";
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }
        var esCorreoValido = Regex.Match(value, emailRegex).Success;
        return esCorreoValido;
    }
}