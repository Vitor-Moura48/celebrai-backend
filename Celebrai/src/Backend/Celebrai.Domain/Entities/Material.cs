namespace Celebrai.Domain.Entities;

public class Material
{
    public int IdMaterial { get; set; }
    public int IdProduto { get; set; }
    public bool VendaIndividual { get; set; }

    public Produto Produto { get; set; } = default!;
}