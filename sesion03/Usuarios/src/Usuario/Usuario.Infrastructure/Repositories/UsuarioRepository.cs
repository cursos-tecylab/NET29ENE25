using Usuario.Domain.Usuarios;

namespace Usuario.Infrastructure.Repositories;

internal sealed class UsuarioRepository : Repository<Domain.Usuarios.Usuario>, IUsuarioRepository
{
    public UsuarioRepository(ApplicationDbContext context) : base(context)
    {
    }
}