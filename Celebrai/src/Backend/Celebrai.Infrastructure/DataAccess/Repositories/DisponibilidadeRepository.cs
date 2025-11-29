using Celebrai.Domain.Entities;
using Celebrai.Domain.Repositories.Disponibilidade;
using Microsoft.EntityFrameworkCore;

namespace Celebrai.Infrastructure.DataAccess.Repositories;

public class DisponibilidadeRepository : IDisponibilidadeWriteOnlyRepository, IDisponibilidadeReadOnlyRepository
{
    private readonly CelebraiDbContext _context;
    public DisponibilidadeRepository(CelebraiDbContext context) => _context = context;

    public async Task AddRange(IEnumerable<Disponibilidade> disponibilidades)
        => await _context.Disponibilidade.AddRangeAsync(disponibilidades);

    public async Task DeleteByFornecedorId(Guid fornecedorId)
    {
        var disponibilidades = await _context.Disponibilidade.Where(d => d.IdFornecedor == fornecedorId).ToListAsync();

        if (disponibilidades.Any())
        {
            _context.Disponibilidade.RemoveRange(disponibilidades);
        }
    }

    public async Task<IList<Disponibilidade>> GetByFornecedorId(Guid fornecedorId)
        => await _context.Disponibilidade.Where(d => d.IdFornecedor == fornecedorId).ToListAsync();
}
