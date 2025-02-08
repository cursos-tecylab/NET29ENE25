using Usuario.Application.Abstractions.Messaging;

namespace Usuario.Application.Usuarios.CrearUsuario;

public sealed record CrearUsuarioCommand
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
) : ICommand<Guid>;