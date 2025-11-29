using Celebrai.Communication.Requests.Fornecedor;
using Celebrai.Domain.Repositories.Fornecedor;
using Celebrai.Domain.Services.LoggedUser;
using MapsterMapper;

namespace Celebrai.Application.UseCases.Fornecedor.Profile;
public class GetFornecedorProfileUseCase : IGetFornecedorProfileUseCase
{
    private readonly IFornecedorReadOnlyRepository _fornecedorReadOnlyRepository;
    private readonly ILoggedUser _loggedUser;
    private readonly IMapper _mapper;

    public GetFornecedorProfileUseCase(
        ILoggedUser loggedUser, 
        IMapper mapper,
        IFornecedorReadOnlyRepository fornecedorReadOnlyRepository)
    {
        _fornecedorReadOnlyRepository = fornecedorReadOnlyRepository;
        _loggedUser = loggedUser;
        _mapper = mapper;
    }

    public async Task<ResponseFornecedorProfileJson> Execute()
    {
        var loggedUser = await _loggedUser.User();

        var fornecedor = await _fornecedorReadOnlyRepository.GetByUserId(loggedUser.IdUsuario);

        var response = _mapper.Map<ResponseFornecedorProfileJson>(fornecedor!);


        var pessoaFisica = await _fornecedorReadOnlyRepository.GetByIdPessoaFisica(fornecedor!.IdFornecedor);

        if (pessoaFisica is not null)
        {
            _mapper.Map(pessoaFisica, response);
        }
        else
        {
            var pessoaJuridica = await _fornecedorReadOnlyRepository.GetByIdPessoaJuridica(fornecedor.IdFornecedor);
            _mapper.Map(pessoaJuridica, response);
        }

        return response;
    }
}
