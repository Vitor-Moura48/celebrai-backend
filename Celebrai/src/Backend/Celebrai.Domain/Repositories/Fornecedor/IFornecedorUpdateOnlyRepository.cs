namespace Celebrai.Domain.Repositories.Fornecedor;
public interface IFornecedorUpdateOnlyRepository
{
    public Task<Entities.Fornecedor?> GetById(Guid fornecedorIdentifier);
    public void Update(Entities.Fornecedor fornecedor);
}
