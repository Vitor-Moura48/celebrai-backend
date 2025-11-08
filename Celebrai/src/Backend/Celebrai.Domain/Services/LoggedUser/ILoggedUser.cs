using Celebrai.Domain.Entities;

namespace Celebrai.Domain.Services.LoggedUser;
public interface ILoggedUser
{
    public Task<Usuario> User();
}
