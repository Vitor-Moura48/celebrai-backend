using Celebrai.Domain.Entities;
using Celebrai.Domain.Enums;
using Celebrai.Domain.Repositories.Categoria;
using Microsoft.EntityFrameworkCore;

namespace Celebrai.Infrastructure.DataAccess.Repositories;
public class CategoriaRepository : ICategoriaReadOnlyRepository
{
    private readonly CelebraiDbContext _context;

    public CategoriaRepository(CelebraiDbContext context) => _context = context;

    public async Task<Categoria> GetCategoriaById(CategoriaEnum categoriaEnum)
        => await _context.Categoria.FirstAsync(x => x.IdCategoria == (int)categoriaEnum);
}
