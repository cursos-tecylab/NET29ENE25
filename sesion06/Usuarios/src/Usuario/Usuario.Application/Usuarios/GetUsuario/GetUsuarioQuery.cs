using Usuario.Application.Abstractions.Messaging;

namespace Usuario.Application.Usuarios.GetUsuario;

public sealed record GetUsuarioQuery(Guid IdUsuario) : IQuery<UsuarioResponse>;