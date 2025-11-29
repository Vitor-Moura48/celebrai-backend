using Celebrai.Communication.Responses.Disponibilidade;
using Celebrai.Domain.Repositories.Disponibilidade;
using Celebrai.Domain.Repositories.Fornecedor;
using Celebrai.Exceptions.ExceptionsBase;
using MapsterMapper;

namespace Celebrai.Application.UseCases.Disponibilidade.GetHoursFornecedor;

public class GetHoursFornecedorUseCase : IGetHoursFornecedorUseCase
{
    private readonly IDisponibilidadeReadOnlyRepository _repository;
    private readonly IMapper _mapper;

    public GetHoursFornecedorUseCase(IDisponibilidadeReadOnlyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<List<ResponseDisponibilidadeJson>> Execute(Guid idFornecedor)
    {
        if (idFornecedor == Guid.Empty)
            throw new NotFoundException("O id do fornecedor é inválido.");

        var availability = await _repository.GetByFornecedorId(idFornecedor);

        if (availability is null || availability.Any() == false)
            throw new NotFoundException("Nenhuma disponibilidade encontrada para o fornecedor informado.");

        var response = _mapper.Map<List<ResponseDisponibilidadeJson>>(availability);

        return response;
    }
}
