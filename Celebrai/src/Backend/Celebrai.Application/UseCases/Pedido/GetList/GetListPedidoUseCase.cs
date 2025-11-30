using Celebrai.Communication.Responses.Pedido;
using Celebrai.Domain.Repositories.Pedido;
using Celebrai.Domain.Repositories.Usuario;
using Celebrai.Domain.Services.LoggedUser;
using MapsterMapper;

namespace Celebrai.Application.UseCases.Pedido.GetList;
public class GetListPedidoUseCase : IGetListPedidoUseCase
{
    private readonly IPedidoReadOnlyRepository _pedidoReadOnlyRepository;
    private readonly IUsuarioReadOnlyRepository _userReadOnlyRepository;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;

    public GetListPedidoUseCase(
        IPedidoReadOnlyRepository pedidoReadOnlyRepository,
        IUsuarioReadOnlyRepository userReadOnlyRepository,
        IMapper mapper,
        ILoggedUser loggedUser)
    {
        _pedidoReadOnlyRepository = pedidoReadOnlyRepository;
        _userReadOnlyRepository = userReadOnlyRepository;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }
    public async Task<ResponseListPedidoJson> Execute()
    {
        var loggedUser = await _loggedUser.User();

        var user = await _userReadOnlyRepository.GetByIdUsuario(loggedUser.IdUsuario);

        var ListPedido = await _pedidoReadOnlyRepository.GetListByUserId(user!.IdUsuario);

        return new ResponseListPedidoJson
        {
            Pedidos = _mapper.Map<List<PedidoItemJson>>(ListPedido)
        };
    }
}