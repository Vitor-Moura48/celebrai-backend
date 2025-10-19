﻿namespace Celebrai.Domain.Entities;

public class Produto
{
    public int IdProduto { get; set; }
    public int IdSubcategoria { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;

    public SubCategoria SubCategoria { get; set; } = default!;
}