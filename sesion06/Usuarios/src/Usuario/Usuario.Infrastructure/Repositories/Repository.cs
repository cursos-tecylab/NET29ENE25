using Microsoft.EntityFrameworkCore;
using Usuario.Domain.Abstractions;

namespace Usuario.Infrastructure.Repositories;

internal abstract class Repository<T> where T : Entity
{
    protected readonly ApplicationDbContext _context;

    protected Repository(ApplicationDbContext context)
    {
        _context = context;
    }   

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<T>()
            .FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);
    }

    public void Add(T entity)
    {
        _context.Add(entity);
    }

    public void Update(T entity)
    {
        _context.Update(entity);
    }

    public void Delete (T entity)
    {
        _context.Remove(entity);
    }
}