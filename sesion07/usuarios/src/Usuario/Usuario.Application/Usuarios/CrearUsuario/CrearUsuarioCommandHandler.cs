using Usuario.Application.Abstractions.Messaging;
using Usuario.Application.Abstractions.Time;
using Usuario.Domain.Abstractions;
using Usuario.Domain.Roles;
using Usuario.Domain.Usuarios;

namespace Usuario.Application.Usuarios.CrearUsuario;

internal sealed class CrearUsuarioCommandHandler : ICommandHandler<CrearUsuarioCommand, Guid>
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly NombreUsuarioServices _nombreUsuarioServices;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IRolRepository _rolRepository;
    public CrearUsuarioCommandHandler(IUsuarioRepository usuarioRepository, IUnitOfWork unitOfWork, NombreUsuarioServices nombreUsuarioServices, IDateTimeProvider dateTimeProvider, IRolRepository rolRepository)
    {
        _usuarioRepository = usuarioRepository;
        _unitOfWork = unitOfWork;
        _nombreUsuarioServices = nombreUsuarioServices;
        _dateTimeProvider = dateTimeProvider;
        _rolRepository = rolRepository;
    }

    public async Task<Result<Guid>> Handle(CrearUsuarioCommand request, CancellationToken cancellationToken)
    {
        var rol = await _rolRepository.GetByNameAsync(request.Rol, cancellationToken);
        if (rol is null)
        {
            return Result.Failure<Guid>(RolErrores.NoEncontrado);
        }

        var usuario = Domain.Usuarios.Usuario.Create(
            request.NombrePersona,
            request.ApellidoPaterno,
            request.ApellidoMaterno,
            Password.Create(request.Password),
            request.FechaNacimiento.ToUniversalTime(),
            CorreoElectronico.Create(request.CorreoElectronico).Value,
            new Direccion(
                request.Pais,
                request.Departamento,
                request.Provincia,
                request.Distrito,
                request.Calle
            ),
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            rol.Id,
            _nombreUsuarioServices
        );

        _usuarioRepository.Add(usuario.Value);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(usuario.Value.Id);

    }
}
