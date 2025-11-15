using Celebrai.Domain.Entities;
using Celebrai.Domain.Enums;
using Celebrai.Domain.Repositories.SubCategoria;
using Microsoft.EntityFrameworkCore;

namespace Celebrai.Infrastructure.DataAccess.Repositories;
public class SubCategoriaRepository : ISubCategoriaReadOnlyRepository
{
    private readonly CelebraiDbContext _context;

    public SubCategoriaRepository(CelebraiDbContext context) => _context = context;

    public async Task<SubCategoria> GetSubCategoriaById(SubCategoriaEnum subCategoria)
        => await _context.SubCategoria.FirstAsync(x => x.IdSubCategoria == (int)subCategoria);
}
