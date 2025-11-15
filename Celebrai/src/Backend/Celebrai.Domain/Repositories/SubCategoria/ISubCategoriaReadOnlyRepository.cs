using Celebrai.Domain.Enums;

namespace Celebrai.Domain.Repositories.SubCategoria;
public interface ISubCategoriaReadOnlyRepository
{
    public Task<Entities.SubCategoria> GetSubCategoriaById(SubCategoriaEnum subCategoria);
}
