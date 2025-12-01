using Celebrai.Communication.Responses.Kit;

namespace Celebrai.Application.UseCases.Kit.GetById;

public interface IGetKitByIdUseCase
{
    public Task<ResponseKitJson> Execute(int id);
}
