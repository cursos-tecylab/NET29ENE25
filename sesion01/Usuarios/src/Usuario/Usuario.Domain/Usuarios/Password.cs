namespace Usuario.Domain.Usuarios;

public record Password
{
    public string Value { get; init; }

    private Password(string value)
    {
        Value = value;
    }

    public static Password Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value)  || value.Length < 9)
        {
            throw new ApplicationException("El password no puede ser nulo o vacío");
        }
        return new Password(value);
    }
}