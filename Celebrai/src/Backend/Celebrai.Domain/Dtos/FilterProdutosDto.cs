namespace Celebrai.Domain.Dtos;
public record FilterProdutosDto
{
    public string Nome { get; init; } = string.Empty;
    public string Categoria { get; init; } = string.Empty;
}
