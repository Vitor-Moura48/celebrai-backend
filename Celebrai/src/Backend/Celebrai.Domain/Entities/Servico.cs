namespace Celebrai.Domain.Entities;

public class Servico
{
    public int IdServico { get; set; }
    public int IdProduto { get; set; }
    public int DuracaoEstimada { get; set; }

    public Produto Produto { get; set; } = default!;
}