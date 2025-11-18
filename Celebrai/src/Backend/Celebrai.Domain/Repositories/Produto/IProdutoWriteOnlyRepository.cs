namespace Celebrai.Domain.Repositories.Produto;
public interface IProdutoWriteOnlyRepository
{
    public Task Add(Entities.Produto product, Entities.FornecedorProduto supplier);
}
