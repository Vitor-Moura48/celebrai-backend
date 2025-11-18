namespace Celebrai.Domain.Repositories.Fornecedor;
public interface IFornecedorReadOnlyRepository
{
    public Task<Entities.Fornecedor?> GetByEmail(string email);
    public Task<bool> ExistActiveFornecedorWithIdentifier(Guid fornecedorIdentifier);
    public Task<bool> ExistActiveFornecedorWithCPF(string cpf);
    public Task<bool> ExistActiveFornecedorWithCNPJ(string cnpj);
}