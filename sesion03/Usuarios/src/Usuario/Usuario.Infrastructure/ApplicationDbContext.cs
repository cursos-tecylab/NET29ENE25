using Microsoft.EntityFrameworkCore;
using Usuario.Domain.Abstractions;

namespace Usuario.Infrastructure;

public sealed class ApplicationDbContext : DbContext, IUnitOfWork
{
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}