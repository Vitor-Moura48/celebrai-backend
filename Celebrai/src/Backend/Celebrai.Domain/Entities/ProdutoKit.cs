namespace Celebrai.Domain.Entities;
public class ProdutoKit
{
    public int IdProduto { get; set; }
    public int IdKit { get; set; }

    public Produto Produto { get; set; } = default!;
    public Kit Kit { get; set; } = default!;
}
