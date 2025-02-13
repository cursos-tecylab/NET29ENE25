using MediatR;
using Usuario.Application.Abstractions.Email;
using Usuario.Domain.Usuarios;
using Usuario.Domain.Usuarios.Event;

namespace Usuario.Application.Usuarios.CrearUsuario;

public class CrearUsuarioCommandDomainEventHandler : INotificationHandler<UserCreateDomainEvent>
{
    private readonly IEmailService _emailService;
    private readonly IUsuarioRepository _usuarioRepository;

    public CrearUsuarioCommandDomainEventHandler(IEmailService emailService, IUsuarioRepository usuarioRepository)
    {
        _emailService = emailService;
        _usuarioRepository = usuarioRepository;
    }

    public async Task Handle(UserCreateDomainEvent notification, CancellationToken cancellationToken)
    {
        var usuario = _usuarioRepository.GetByIdAsync(notification.IdUsuario,cancellationToken);
        if(usuario is null)
        {
           return;
        }

        await _emailService.SendEmailAsync(
            usuario.Result!.CorreoElectronico!.Value,
            "Bienvenido a Tecylab", 
            $"Su usuario es {usuario.Result!.NombreUsuario} y fue creado correctamente el {usuario.Result.FechaUltimoCambio}");
    }
}