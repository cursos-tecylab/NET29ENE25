using Usuario.Domain.Abstractions;
using Usuario.Domain.Roles;
using Usuario.Domain.Shared;
using Usuario.Domain.Usuarios.Event;

namespace Usuario.Domain.Usuarios;

public class Usuario : Entity
{
    private readonly List<DobleFactorAutenticacion> _dobleFactorAutenticaciones = new();
    public IReadOnlyList<DobleFactorAutenticacion> DobleFactorAutenticaciones => _dobleFactorAutenticaciones.AsReadOnly();
    
    public string? NombrePersona { get; private set; }
    public string? ApellidoPaterno { get; private set; }
    public string? ApellidoMaterno { get; private set; }
    public Password? Password { get; private set; }
    public NombreUsuario? NombreUsuario { get; private set; }
    public DateTime FechaNacimiento { get; private set; }
    public CorreoElectronico?  CorreoElectronico { get; private set; }
    public Direccion? Direccion { get; private set; }
    public Estados Estado { get; private set; }
    public DateTime FechaUltimoCambio   { get; private set; }
    public Rol? Rol { get; private set; }
    public Guid RoldId { get; private set; }

    private Usuario(
        Guid id,
        NombreUsuario nombreUsuario,
        string nombrePersona,
        string apellidoPaterno,
        string apellidoMaterno,
        Password password,
        DateTime fechaNacimiento,
        CorreoElectronico correoElectronico,
        Direccion direccion,
        Estados estado,
        DateTime fechaUltimoCambio,
        Guid rolId) : base(id)
    {
        NombreUsuario = nombreUsuario;
        NombrePersona = nombrePersona;
        ApellidoPaterno = apellidoPaterno;
        ApellidoMaterno = apellidoMaterno;
        Password = password;
        FechaNacimiento = fechaNacimiento;
        CorreoElectronico = correoElectronico;
        Direccion = direccion;
        Estado = estado;
        FechaUltimoCambio = fechaUltimoCambio;
        RoldId = rolId;
    }
    
    public static Result<Usuario> Create(
        string nombrePersona,
        string apellidoPaterno,
        string apellidoMaterno,
        Password password,
        DateTime fechaNacimiento,
        CorreoElectronico correoElectronico,
        Direccion direccion,
        DateTime fechaUltimoCambio,
        Guid rolId,
        NombreUsuarioServices nombreUsuarioServices)
    {
        var nombreUsuarioResult = nombreUsuarioServices.GenerarNombreUsuario(nombrePersona, apellidoPaterno);

        var usuario = new Usuario(
            Guid.NewGuid(),
            nombreUsuarioResult.Value,
            nombrePersona,
            apellidoPaterno,
            apellidoMaterno,
            password,
            fechaNacimiento,
            correoElectronico,
            direccion,
            Estados.Activo,
            fechaUltimoCambio,
            rolId);

        usuario.RaiseDomainEvent(new UserCreateDomainEvent(usuario.Id));

        return Result.Success(usuario);
    }


    public Result Inactivar(DateTime utcNow)
    {
        if (Estado == Estados.Inactivo)
        {
            return Result.Failure(UsuarioErrores.UsuarioInactivo);
        }

        Estado = Estados.Inactivo;
        FechaUltimoCambio = utcNow;
        return Result.Success();
    }

    public Result AgregarDobleFactor(TipoDobleFactor tipo, string codigo, DateTime fechaCreacion)
    {
        if (_dobleFactorAutenticaciones.Any(x => x.Tipo == tipo && x.Estado == Estados.Activo))
        {
            return Result.Failure(UsuarioErrores.MetodoDobleFactorExiste);
        }

        var dobleFactorResult = DobleFactorAutenticacion.Registrar(Id, tipo, codigo, fechaCreacion);
        _dobleFactorAutenticaciones.Add(dobleFactorResult.Value);
        return Result.Success();
    }

    public Result DesactivarDobleFactor(Guid idMetodo)
    {
        var dobleFactor = _dobleFactorAutenticaciones.FirstOrDefault(x => x.Id == idMetodo && x.Estado == Estados.Activo);
        if (dobleFactor is null)
        {
            return Result.Failure(UsuarioErrores.MetodoDobleFactorNoExiste);
        }
        dobleFactor.Desactivar();
        return Result.Success();
    }

}