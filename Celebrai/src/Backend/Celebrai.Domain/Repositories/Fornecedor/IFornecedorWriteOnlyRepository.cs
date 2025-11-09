namespace Celebrai.Domain.Repositories.Fornecedor;
public interface IFornecedorWriteOnlyRepository
{
    public Task Add(Entities.Fornecedor fornecedor);
    public Task AddPessoaFisica(Entities.PessoaFisica pessoaFisica);
    public Task AddPessoaJuridica(Entities.PessoaJuridica pessoaJuridica);
}