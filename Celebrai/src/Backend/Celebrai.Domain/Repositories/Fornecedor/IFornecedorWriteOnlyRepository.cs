namespace Celebrai.Domain.Repositories.Fornecedor;
public interface IFornecedorWriteOnlyRepository
{
    public Task Add(Entities.Fornecedor fornecedor);
}
