using Celebrai.Domain.Enums;

namespace Celebrai.Domain.Entities;

public class ModalidadeEntrega
{
    public int IdModalidadeEntrega { get; set; }
    public TipoEntrega Metodo { get; set; }
}