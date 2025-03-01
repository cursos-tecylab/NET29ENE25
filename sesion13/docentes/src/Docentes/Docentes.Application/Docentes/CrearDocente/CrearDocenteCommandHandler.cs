using Docentes.Application.Abstractions.Messaging;
using Docentes.Application.Service;
using Docentes.Domain.Abstractions;
using Docentes.Domain.Docentes;

namespace Docentes.Application.Docentes.CrearDocente;

internal sealed class CrearDocenteCommandHandler :
ICommandHandler<CrearDocenteCommand, Guid>
{
    private readonly IDocenteRepository _docenteRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUsuarioService _usuarioService;
    private readonly ICursosService _cursosService;
    private readonly ICacheService _cacheService;

    public CrearDocenteCommandHandler(IDocenteRepository docenteRepository, IUnitOfWork unitOfWork, IUsuarioService usuarioService, ICursosService cursosService, ICacheService cacheService)
    {
        _docenteRepository = docenteRepository;
        _unitOfWork = unitOfWork;
        _usuarioService = usuarioService;
        _cursosService = cursosService;
        _cacheService = cacheService;
    }

    public async Task<Result<Guid>> Handle(CrearDocenteCommand request, CancellationToken cancellationToken)
    {
        if (!await _usuarioService.UsuarioExisteAsync(request.usuarioId, cancellationToken))
        {
            return Result.Failure<Guid>(new Error("UsuarioNotFound","El usuario no existe."));
        }

        var cacheKey = $"curso_{request.especialidadId}";
        var cursoExist = await _cacheService.GetCacheValueAsync<bool>(cacheKey); // buscamos en cache

        if (!cursoExist)
        {
            cursoExist = await _cursosService.CursoExisteAsync(request.especialidadId, cancellationToken); // buscar en BD
            var expirationTime = TimeSpan.FromMinutes(5); // tiempo que va a permanecer en redis
            await _cacheService.SetCacheValueAsync(cacheKey, cursoExist, expirationTime);
        }

        if (!cursoExist) // no existe en cache ni en BD
        {
            return Result.Failure<Guid>(new Error("CursoNotFound","El curso no existe."));
        }
    
        var docente = Docente.Create(
            request.usuarioId,
            request.especialidadId
        );

        _docenteRepository.Add(docente.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return docente.Value.Id;
    }
}