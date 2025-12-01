using Celebrai.Communication.Requests.Kit;
using Celebrai.Communication.Responses.Kit;

namespace Celebrai.Application.UseCases.Kit.Register;

public interface IRegisterKitUseCase
{
    public Task<ResponseRegisteredKitJson> Execute(RequestKitJson request);
}