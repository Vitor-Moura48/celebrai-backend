namespace Celebrai.Domain.Repositories;
public interface IUnitOfWork
{
    public Task Commit();
}
