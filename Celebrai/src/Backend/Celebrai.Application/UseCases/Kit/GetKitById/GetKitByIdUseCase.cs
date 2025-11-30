using Celebrai.Application.UseCases.Kit.GetById;
using Celebrai.Communication.Responses.Kit;
using Celebrai.Domain.Repositories.Kit;
using Celebrai.Exceptions.ExceptionsBase;
using MapsterMapper;

namespace Celebrai.Application.UseCases.Kit.GetKitById;

public class GetKitByIdUseCase : IGetKitByIdUseCase
{
    private readonly IKitReadOnlyRepository _kitReadOnlyRepository;
    private readonly IMapper _mapper;

    public GetKitByIdUseCase(
        IKitReadOnlyRepository kitReadOnlyRepository,
        IMapper mapper)
    {
        _kitReadOnlyRepository = kitReadOnlyRepository;
        _mapper = mapper;
    }
    public async Task<ResponseKitJson> Execute(int id)
    {
        var kit = await _kitReadOnlyRepository.GetByIdAsync(id);

        if (kit is null)
            throw new NotFoundException("Kit não encontrado.");

        return _mapper.Map<ResponseKitJson>(kit);
    }
}
