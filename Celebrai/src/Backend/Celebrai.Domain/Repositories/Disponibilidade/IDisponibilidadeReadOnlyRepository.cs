namespace Celebrai.Domain.Repositories.Disponibilidade;

public interface IDisponibilidadeReadOnlyRepository
{
    public Task<IList<Entities.Disponibilidade>> GetByFornecedorId(Guid fornecedorId);
}
