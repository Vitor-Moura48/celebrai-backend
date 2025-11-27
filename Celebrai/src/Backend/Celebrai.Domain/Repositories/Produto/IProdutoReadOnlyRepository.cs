using Celebrai.Domain.Dtos;

namespace Celebrai.Domain.Repositories.Produto;
public interface IProdutoReadOnlyRepository
{
    public Task<Entities.Produto?> GetProdutoByIdentifier(int id);
    public Task<IList<Entities.Produto>> GetProdutosList(int? page);
    public Task<IList<Entities.Produto>> GetProdutosWithFilters(int? page, FilterProdutosDto filter);
}