using Usuario.Domain.Abstractions;

namespace Usuario.Domain.Usuario;

public class Usuario : Entity
{
    public ApellidoPaterno? ApellidoPaterno { get; private set; }
    public Password? Password { get; private set; }

}