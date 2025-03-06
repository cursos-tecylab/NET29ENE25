using Dapper;
using Usuario.Application.Abstractions.Data;
using Usuario.Application.Abstractions.Messaging;
using Usuario.Domain.Abstractions;

namespace Usuario.Application.Usuarios.GetUsuario;

internal sealed class GetUsuarioQueryHandler : IQueryHandler<GetUsuarioQuery, UsuarioResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetUsuarioQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<UsuarioResponse>> Handle(
        GetUsuarioQuery request, 
        CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        var sql  = """
             SELECT 
                u.id AS Id,
                u.nombre_persona AS NombrePersona,
                u.nombre_usuario AS NombreUsuario,
                u.apellido_paterno AS ApellidoPaterno,
                u.apellido_materno AS ApellidoMaterno,
                u.fecha_nacimiento AS FechaNacimiento,
                u.correo_electronico AS CorreoElectronico,
                u.direccion_pais AS Pais,
                u.direccion_departamento AS Departamento,
                u.direccion_provincia AS Provincia,
                u.direccion_distrito AS Distrito,
                u.direccion_calle AS Calle,
                r.nombre_rol AS Rol,
                u.fecha_ultimo_cambio AS FechaUltimoCambio,
                u.estado AS Estado
        FROM usuarios u
        INNER JOIN roles r ON u.rol_id = r.id
        WHERE u.id = @IdUsuario
        """;

        var usuario = await connection.QueryFirstOrDefaultAsync<UsuarioResponse>(
            sql,
            new { request.IdUsuario });

        return usuario!;
    }
}