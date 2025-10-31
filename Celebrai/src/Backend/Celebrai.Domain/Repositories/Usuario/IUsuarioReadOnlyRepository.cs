namespace Celebrai.Domain.Repositories.Usuario;
public interface IUsuarioReadOnlyRepository
{
    public Task<Entities.Usuario?> GetByEmail(string email);
    public Task<bool> ExistUserWithEmail(string email);
    public Task<Entities.Usuario?> GetByAuthProviderIdAsync(string authProviderId);
}
