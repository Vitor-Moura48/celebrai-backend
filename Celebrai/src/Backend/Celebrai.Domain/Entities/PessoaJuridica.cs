namespace Celebrai.Domain.Entities;
public class PessoaJuridica
{
    public string Cnpj { get; set; } = string.Empty;
    public string RazaoSocial { get; set; } = string.Empty;
    public Guid IdFornecedor { get; set; }

    public Fornecedor Fornecedor { get; set; } = default!;
}
