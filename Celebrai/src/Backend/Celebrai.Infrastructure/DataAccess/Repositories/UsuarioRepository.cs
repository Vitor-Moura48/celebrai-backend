using Celebrai.Domain.Entities;
using Celebrai.Domain.Repositories.Usuario;
using Microsoft.EntityFrameworkCore;

namespace Celebrai.Infrastructure.DataAccess.Repositories;
public class UsuarioRepository : IUsuarioReadOnlyRepository, IUsuarioWriteOnlyRepository, IUsuarioUpdateOnlyRepository
{
    private readonly CelebraiDbContext _context;

    public UsuarioRepository(CelebraiDbContext context) => _context = context;

    public async Task Add(Usuario user) => await _context.Usuario.AddAsync(user);

    public async Task<bool> ExistUserWithEmail(string email) => await _context.Usuario.AnyAsync(user => user.Email.Equals(email));

    public Task<Usuario?> GetByAuthProviderIdAsync(string authProviderId)
        => _context.Usuario.FirstOrDefaultAsync(user => user.IdExterno.Equals(authProviderId));

    public async Task<Usuario?> GetByEmail(string email)
        => await _context.Usuario.FirstOrDefaultAsync(user => user.Email.Equals(email));

    public void Update(Usuario user) => _context.Usuario.Update(user);
}
