using Celebrai.Domain.Entities;
using Celebrai.Domain.Repositories.Usuario;
using Microsoft.EntityFrameworkCore;

namespace Celebrai.Infrastructure.DataAccess.Repositories;
public class UsuarioRepository : IUsuarioReadOnlyRepository, IUsuarioWriteOnlyRepository, IUsuarioUpdateOnlyRepository
{
    private readonly CelebraiDbContext _context;

    public UsuarioRepository(CelebraiDbContext context) => _context = context;

    public async Task Add(Usuario user) => await _context.Usuario.AddAsync(user);

    public async Task<bool> ExistActiveUserWithEmail(string email) => await _context.Usuario.AnyAsync(user => user.Email.Equals(email) && user.Ativo);

    public async Task<bool> ExistUserWithCpf(string cpf) => await _context.Usuario.AnyAsync(user => user.CpfUsuario.Equals(cpf));

    public async Task<bool> ExistActiveUserWithIdentifier(Guid userIdentifier) => await _context.Usuario.AnyAsync(user => user.IdUsuario.Equals(userIdentifier) && user.Ativo);

    public async Task<Usuario?> GetByEmail(string email)
        => await _context.Usuario.FirstOrDefaultAsync(user => user.Email.Equals(email));

    public async Task<Usuario?> GetById(Guid userIdentifier) => await _context.Usuario.FirstOrDefaultAsync(user => user.IdUsuario == userIdentifier);

    public void Update(Usuario user) => _context.Usuario.Update(user);

    public async Task<Usuario?> GetByIdUsuario(Guid userIdentifier)
        => await _context.Usuario.FirstOrDefaultAsync(user => user.IdUsuario == userIdentifier);
}
