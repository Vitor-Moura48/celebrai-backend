using Celebrai.Domain.Entities;
using Celebrai.Domain.Repositories.Fornecedor;
using Microsoft.EntityFrameworkCore;

namespace Celebrai.Infrastructure.DataAccess.Repositories;
public class FornecedorRepository : IFornecedorReadOnlyRepository, IFornecedorWriteOnlyRepository, IFornecedorUpdateOnlyRepository
{
    private readonly CelebraiDbContext _context;

    public FornecedorRepository(CelebraiDbContext context) => _context = context;

    public async Task Add(Fornecedor fornecedor) => await _context.Fornecedor.AddAsync(fornecedor);

    public async Task<bool> ExistActiveFornecedorWithEmail(string email) => await _context.Fornecedor.AnyAsync(fornecedor => fornecedor.Usuario.Email.Equals(email) && fornecedor.Ativo);

    public async Task<Fornecedor?> GetByEmail(string email)
        => await _context.Fornecedor.FirstOrDefaultAsync(fornecedor => fornecedor.Usuario.Email.Equals(email));

    public async Task<Fornecedor?> GetById(Guid fornecedorIdentifier) => await _context.Fornecedor.FirstOrDefaultAsync(fornecedor => fornecedor.IdFornecedor == fornecedorIdentifier);

    public void Update(Fornecedor fornecedor) => _context.Fornecedor.Update(fornecedor);
}
