using Celebrai.Domain.Entities;
using Celebrai.Domain.Repositories.Fornecedor;
using Microsoft.EntityFrameworkCore;

namespace Celebrai.Infrastructure.DataAccess.Repositories;
public class FornecedorRepository : IFornecedorReadOnlyRepository, IFornecedorWriteOnlyRepository, IFornecedorUpdateOnlyRepository
{
    private readonly CelebraiDbContext _context;

    public FornecedorRepository(CelebraiDbContext context) => _context = context;

    public async Task Add(Fornecedor fornecedor)
    {
        _context.Attach(fornecedor.Usuario);
        await _context.Fornecedor.AddAsync(fornecedor);
    }

    public async Task<bool> ExistActiveFornecedorWithIdentifier(Guid fornecedorIdentifier) => await _context.Fornecedor.AnyAsync(user => user.IdUsuario.Equals(fornecedorIdentifier) && user.Ativo);
    public async Task AddPessoaFisica(PessoaFisica pessoaFisica)
    {
        await _context.PessoaFisica.AddAsync(pessoaFisica);
    }
    public async Task AddPessoaJuridica(PessoaJuridica pessoaJuridica)
    {
        await _context.PessoaJuridica.AddAsync(pessoaJuridica);
    }

    public async Task<bool> ExistActiveFornecedorWithCPF(string cpf) => await _context.PessoaFisica.AnyAsync(pf => pf.Cpf.Equals(cpf) && pf.Fornecedor.Ativo);
    public async Task<bool> ExistActiveFornecedorWithCNPJ(string cnpj) => await _context.PessoaJuridica.AnyAsync(pj => pj.Cnpj.Equals(cnpj) && pj.Fornecedor.Ativo);

    public async Task<Fornecedor?> GetByEmail(string email)
        => await _context.Fornecedor.FirstOrDefaultAsync(fornecedor => fornecedor.Usuario.Email.Equals(email));

    public async Task<Fornecedor?> GetByUserId(Guid userId)
        => await _context.Fornecedor.FirstOrDefaultAsync(fornecedor => fornecedor.IdUsuario == userId);

    public async Task<Fornecedor?> GetById(Guid fornecedorIdentifier) => await _context.Fornecedor.FirstOrDefaultAsync(fornecedor => fornecedor.IdUsuario == fornecedorIdentifier);

    public void Update(Fornecedor fornecedor) => _context.Fornecedor.Update(fornecedor);

    public async Task<FornecedorProduto?> GetByProductId(int productId)
        => await _context.FornecedorProduto.AsNoTracking().FirstOrDefaultAsync(fp => fp.IdProduto == productId);

    public async Task<Fornecedor?> GetByIdFornecedor(Guid fornecedorIdentifier)
        => await _context.Fornecedor.AsNoTracking().FirstOrDefaultAsync(fornecedor => fornecedor.IdFornecedor == fornecedorIdentifier);

    public async Task<PessoaFisica?> GetByIdPessoaFisica(Guid fornecedorIdentifier)
        => await _context.PessoaFisica.AsNoTracking().FirstOrDefaultAsync(PessoaFisica => PessoaFisica.IdFornecedor == fornecedorIdentifier);

    public async Task<PessoaJuridica?> GetByIdPessoaJuridica(Guid fornecedorIdentifier)
            => await _context.PessoaJuridica.AsNoTracking().FirstOrDefaultAsync(PessoaJuridica => PessoaJuridica.IdFornecedor == fornecedorIdentifier);

}
