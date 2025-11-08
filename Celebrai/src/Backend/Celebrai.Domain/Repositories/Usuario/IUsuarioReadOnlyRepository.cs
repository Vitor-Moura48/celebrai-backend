namespace Celebrai.Domain.Repositories.Usuario;
public interface IUsuarioReadOnlyRepository
{
    public Task<Entities.Usuario?> GetByEmail(string email);
    public Task<bool> ExistActiveUserWithEmail(string email);
    public Task<bool> ExistActiveUserWithIdentifier(Guid userIdentifier);
    public Task<bool> ExistUserWithCpf(string cpf);
}
