using Cursos.Application.Cursos.ObtenerCurso;
using Grpc.Core;
using MediatR;

namespace Cursos.Api.gRPC;

public class CursosGrpcService : CursosService.CursosServiceBase
{
    private readonly ISender _sender;

    public CursosGrpcService(ISender sender)
    {
        _sender = sender;
    }

    public override async Task<CursoResponse> CursosExists(
        CursoRequest request, 
        ServerCallContext context)
    {
        var cursoId = Guid.Parse(request.CursoId);
        var query = new GetCursoQuery(cursoId);
        var result = await _sender.Send(query, context.CancellationToken);
        return new CursoResponse
        {
            Exists = result.IsSuccess
        };
    }

}