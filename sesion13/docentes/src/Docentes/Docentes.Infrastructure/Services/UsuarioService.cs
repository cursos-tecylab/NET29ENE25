using Docentes.Application.Service;

namespace Docentes.Infrastructure.Services;

public class UsuarioService : IUsuarioService
{
    private readonly HttpClient _httpClient;

    public UsuarioService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> UsuarioExisteAsync(Guid usuarioId, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetAsync($"usuarios/{usuarioId}", cancellationToken);
        return response.IsSuccessStatusCode;
    }
}