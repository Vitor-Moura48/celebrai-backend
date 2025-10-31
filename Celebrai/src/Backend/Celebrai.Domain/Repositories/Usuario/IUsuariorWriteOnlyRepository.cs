namespace Celebrai.Domain.Repositories.Usuario;
public interface IUsuarioWriteOnlyRepository
{
    public Task Add(Entities.Usuario user);
}
