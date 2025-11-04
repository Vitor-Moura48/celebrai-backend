namespace Celebrai.Domain.Repositories.Usuario;
public interface IUsuarioUpdateOnlyRepository
{
    public Task<Entities.Usuario?> GetById(Guid userIdentifier);
    public void Update(Entities.Usuario user);
}
