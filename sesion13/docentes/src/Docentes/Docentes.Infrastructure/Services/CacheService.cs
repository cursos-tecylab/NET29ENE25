using System.Text.Json;
using Docentes.Application.Service;
using StackExchange.Redis;

namespace Docentes.Infrastructure.Services;

public class CacheService : ICacheService
{
    private readonly IConnectionMultiplexer _connection;

    public CacheService(IConnectionMultiplexer connection)
    {
        _connection = connection;
    }

    public async Task<T?> GetCacheValueAsync<T>(string key)
    {
       var db = _connection.GetDatabase();
       var value = await db.StringGetAsync(key);
       if (value.IsNullOrEmpty)
       {
            return default;
       }
       return JsonSerializer.Deserialize<T>(value!);
    }

    public async Task SetCacheValueAsync<T>(string key, T value, TimeSpan? expirationTime = null)
    {
        var db = _connection.GetDatabase();
        await db.StringSetAsync(key, JsonSerializer.Serialize(value), expirationTime);
    }
}