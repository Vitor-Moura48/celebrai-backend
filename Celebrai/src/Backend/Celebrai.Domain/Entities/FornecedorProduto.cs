namespace Celebrai.Domain.Entities;

public class FornecedorProduto
{
    public Guid IdFornecedor { get; set; }
    public int IdProduto { get; set; }

    public Fornecedor Fornecedor { get; set; } = default!;
    public Produto Produto { get; set; } = default!;
}