using Usuario.Domain.Abstractions;

namespace Usuario.Domain.Usuarios;

public class NombreUsuarioServices
{
    public Result<NombreUsuario> GenerarNombreUsuario(string nombrePersona, string apellidoPaterno)
    {
        var inicialNombre = nombrePersona.Substring(0, 1);  
        var nombreUsuario = NombreUsuario.Create($"{inicialNombre}{apellidoPaterno.Trim()}");
        return nombreUsuario;
    }
}