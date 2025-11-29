namespace Celebrai.Domain.Repositories.Fornecedor;
public interface IFornecedorReadOnlyRepository
{
    public Task<Entities.Fornecedor?> GetByEmail(string email);
    public Task<Entities.Fornecedor?> GetByUserId(Guid userId);
    public Task<Entities.Fornecedor?> GetByIdFornecedor(Guid fornecedorIdentifier);
    public Task<Entities.PessoaFisica?> GetByIdPessoaFisica(Guid pessoaFisicaIdentifier);
    public Task<Entities.PessoaJuridica?> GetByIdPessoaJuridica(Guid pessoaJurucicaIdentifier);
    public Task<Entities.FornecedorProduto?> GetByProductId(int productId);
    public Task<bool> ExistActiveFornecedorWithIdentifier(Guid fornecedorIdentifier);
    public Task<bool> ExistActiveFornecedorWithCPF(string cpf);
    public Task<bool> ExistActiveFornecedorWithCNPJ(string cnpj);
}