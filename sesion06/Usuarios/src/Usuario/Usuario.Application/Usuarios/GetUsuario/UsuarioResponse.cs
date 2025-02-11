namespace Usuario.Application.Usuarios.GetUsuario;

public record UsuarioResponse
(
    Guid Id,
    string NombrePersona,
    string NombreUsuario,
    string ApellidoPaterno,
    string ApellidoMaterno,
    DateTime FechaNacimiento,
    string CorreoElectronico,
    string Pais,
    string Departamento,
    string Provincia,
    string Distrito,
    string Calle,
    string Rol,
    DateTime FechaUltimoCambio,
    string Estado
);