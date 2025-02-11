using Microsoft.EntityFrameworkCore;
using Usuario.Domain.Roles;

namespace Usuario.Infrastructure.Repositories;

internal sealed class RolRepository : Repository<Rol>, IRolRepository
{
    public RolRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Rol?> GetByNameAsync(string nombreRol, CancellationToken cancellationToken)
    {
       return await _context.Set<Rol>().FirstOrDefaultAsync(
            rol => rol.NombreRol!.Equals(nombreRol), cancellationToken
       );
    }
}