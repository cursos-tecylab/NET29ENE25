using Usuario.Domain.Abstractions;
using Usuario.Domain.Shared;

namespace Usuario.Domain.Usuarios;

public class DobleFactorAutenticacion : Entity
{
    public Guid UsuarioId { get; private set; }
    public TipoDobleFactor Tipo { get; private set; }
    public string? Codigo { get; private set; }
    public Estados Estado { get; private set; }
    public DateTime FechaCreacion { get; private set; }

    private DobleFactorAutenticacion(
        Guid id,
        Guid usuarioId,
        TipoDobleFactor tipo,
        string codigo,
        DateTime fechaCreacion) : base(id)
    {
        UsuarioId = usuarioId;
        Tipo = tipo;
        Codigo = codigo;
        Estado = Estados.Activo;
        FechaCreacion = fechaCreacion;
    }

    public static Result<DobleFactorAutenticacion> Registrar(
        Guid usuarioId,
        TipoDobleFactor tipo,
        string codigo,
        DateTime fechaCreacion)
    {
        return new DobleFactorAutenticacion(
            Guid.NewGuid(),
            usuarioId,
            tipo,
            codigo,
            fechaCreacion
        );
    }

    public Result Desactivar()
    {
        Estado = Estados.Inactivo;
        return Result.Success();
    }

}