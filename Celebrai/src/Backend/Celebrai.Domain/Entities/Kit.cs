namespace Celebrai.Domain.Entities;
public class Kit
{
    public int IdKit { get; set; }
    public string Nome { get; set; } = string.Empty;
    public bool VendaIndividual { get; set; } = false;
    public decimal KitPreco { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public int QuantidadeAluguelPorDia { get; set; } = 1;

}
