using Celebrai.Domain.Entities;
using Celebrai.Domain.Enums;

namespace Celebrai.Domain.Repositories.Categoria;
public interface ICategoriaReadOnlyRepository
{
    public Task<Entities.Categoria> GetCategoriaById(CategoriaEnum categoriaEnum);
}
