using Celebrai.Communication.Requests.Disponibilidade;
using Celebrai.Communication.Responses.Disponibilidade;

namespace Celebrai.Application.UseCases.Disponibilidade;

public interface IRegisterDisponibilidadeUseCase
{
    public Task Execute(RequestRegistedDisponibilidadeJson request);
}
