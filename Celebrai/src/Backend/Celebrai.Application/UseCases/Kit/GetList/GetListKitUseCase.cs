using Celebrai.Application.UseCases.Produto.GetList;
using Celebrai.Communication.Responses.Kit;
using Celebrai.Domain.Repositories.Kit;
using MapsterMapper;

namespace Celebrai.Application.UseCases.Kit.GetList
{
    public class GetListKitUseCase : IGetListKitUseCase
    {
        private readonly IKitReadOnlyRepository _kitReadOnlyRepository;
        private readonly IMapper _mapper;

        public GetListKitUseCase(
            IKitReadOnlyRepository kitReadOnlyRepository,
            IMapper mapper
        )
        {
            _kitReadOnlyRepository = kitReadOnlyRepository;
            _mapper = mapper;   
        }
        public async Task<IList<ResponseKitJson>> Execute(int? page)
        {
            var kits = await _kitReadOnlyRepository.GetKitsList(page);

            var response = _mapper.Map<IList<ResponseKitJson>>(kits);

            return response;
        }
    }
}