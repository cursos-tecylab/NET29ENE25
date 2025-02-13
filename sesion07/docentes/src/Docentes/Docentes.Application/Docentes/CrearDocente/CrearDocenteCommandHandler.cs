using Docentes.Application.Abstractions.Messaging;
using Docentes.Domain.Abstractions;
using Docentes.Domain.Docentes;

namespace Docentes.Application.Docentes.CrearDocente;

internal sealed class CrearDocenteCommandHandler :
ICommandHandler<CrearDocenteCommand, Guid>
{
    private readonly IDocenteRepository _docenteRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CrearDocenteCommandHandler(IDocenteRepository docenteRepository, IUnitOfWork unitOfWork)
    {
        _docenteRepository = docenteRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CrearDocenteCommand request, CancellationToken cancellationToken)
    {
    
        var docente = Docente.Create(
            request.usuarioId,
            request.especialidadId
        );

        _docenteRepository.Add(docente.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return docente.Value.Id;
    }
}