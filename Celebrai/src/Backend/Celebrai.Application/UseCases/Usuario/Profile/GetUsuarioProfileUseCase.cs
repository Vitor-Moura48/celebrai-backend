using Celebrai.Communication.Requests.Usuario;
using Celebrai.Domain.Services.LoggedUser;
using MapsterMapper;

namespace Celebrai.Application.UseCases.Usuario.Profile;
public class GetUsuarioProfileUseCase : IGetUsuarioProfileUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IMapper _mapper;

    public GetUsuarioProfileUseCase(ILoggedUser loggedUser, IMapper mapper)
    {
        _loggedUser = loggedUser;
        _mapper = mapper;
    }

    public async Task<ResponseUsuarioProfileJson> Execute()
    {
        var user = await _loggedUser.User();

        return _mapper.Map<ResponseUsuarioProfileJson>(user);
    }
}
