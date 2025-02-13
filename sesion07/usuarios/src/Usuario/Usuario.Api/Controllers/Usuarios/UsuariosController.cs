using MediatR;
using Microsoft.AspNetCore.Mvc;
using Usuario.Application.Usuarios.CrearUsuario;
using Usuario.Application.Usuarios.GetUsuario;

namespace Usuario.Api.Controllers.Usuarios;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly ISender _send;

    public UsuariosController(ISender send)
    {
        _send = send;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(
        CrearUsuarioRequest request,
        CancellationToken cancellationToken
    )
    {
        var commad = new CrearUsuarioCommand(
            request.NombrePersona,
            request.ApellidoPaterno,
            request.ApellidoMaterno,
            request.Password,
            request.FechaNacimiento,
            request.CorreoElectronico,
            request.Pais,
            request.Departamento,
            request.Provincia,
            request.Distrito,
            request.Calle,
            request.Rol
        );

        var result = await _send.Send(commad,cancellationToken);

        if(result.IsSuccess)
        {
            return CreatedAtAction(nameof(GetUser),new { id = result.Value } , result.Value  );
        }
        return BadRequest(result.Error);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetUser(
        Guid id,
        CancellationToken cancellationToken
    )
    {
        var query = new GetUsuarioQuery(id);
        var user = await _send.Send(query,cancellationToken);
        return user is not null ? Ok(user) : NotFound();
    }

}