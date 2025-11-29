namespace Celebrai.Domain.Repositories.Disponibilidade;

public interface IDisponibilidadeWriteOnlyRepository
{
    public Task DeleteByFornecedorId(Guid fornecedorId);
    public Task AddRange(IEnumerable<Entities.Disponibilidade> disponibilidades);
}
