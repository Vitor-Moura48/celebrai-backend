namespace Celebrai.Domain.Repositories.Kit;

public interface IKitReadOnlyRepository
{
    public Task<IList<Entities.Kit>> GetKitsList(int? page);
    public Task<Entities.Kit?> GetByIdAsync(int id);    
    public Task<bool> ExistKitWithIdentifier(int id);
}
