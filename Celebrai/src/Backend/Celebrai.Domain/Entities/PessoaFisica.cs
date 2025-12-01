namespace Celebrai.Domain.Entities;
public class PessoaFisica
{
    public string Cpf { get; set; } = string.Empty;
    public string NomeCompleto { get; set; } = string.Empty;
    public Guid IdFornecedor { get; set; }

    public Fornecedor Fornecedor { get; set; } = default!;
}
