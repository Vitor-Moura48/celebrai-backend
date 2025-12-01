using Celebrai.Domain.Entities;

namespace Celebrai.Domain.Repositories.Kit;

public interface IKitWriteOnlyRepository
{
    public Task Add(Entities.Kit kit, IEnumerable<ProdutoKit> produtosDoKit);
}