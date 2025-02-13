namespace Usuario.Api.Controllers.Usuarios;

public record CrearUsuarioRequest
(
    string NombrePersona,
    string ApellidoPaterno,
    string ApellidoMaterno,
    string Password,
    DateTime FechaNacimiento,
    string CorreoElectronico,
    string Pais,
    string Departamento,
    string Provincia,
    string Distrito,
    string Calle,
    string Rol
);