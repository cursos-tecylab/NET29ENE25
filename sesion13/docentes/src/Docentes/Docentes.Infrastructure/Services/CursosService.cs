using Docentes.Application.Service;

namespace Docentes.Infrastructure.Services;

public class CursosService : ICursosService
{
    private readonly HttpClient _httpClient;

    public CursosService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> CursoExisteAsync(Guid cursoId, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetAsync($"cursos/{cursoId}", cancellationToken);
        return response.IsSuccessStatusCode;

    }
}