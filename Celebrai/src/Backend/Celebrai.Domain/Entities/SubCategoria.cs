namespace Celebrai.Domain.Entities;

public class SubCategoria
{
    public int IdSubCategoria { get; set; }
    public int IdCategoria { get; set; }
    public string Nome { get; set; } = string.Empty;

    public Categoria Categoria { get; set; } = default!;
}